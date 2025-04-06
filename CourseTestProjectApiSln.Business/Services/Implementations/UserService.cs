using AutoMapper;
using CourseTestProjectApiSln.Business.DTOs.BaseResponseModel;
using CourseTestProjectApiSln.Business.DTOs.User;
using CourseTestProjectApiSln.Business.Services.Abstractions;
using CourseTestProjectApiSln.Business.Services.Abstractions.Infra;
using CourseTestProjectApiSln.DataAccess.Entities;
using CourseTestProjectApiSln.DataAccess.Enums;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstactions;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstractions;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstractions.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace CourseTestProjectApiSln.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public UserService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository)
        {
            _unitOfWork = unitOfWork;
            _appUserRepository = _unitOfWork.GetRepository<IUserRepository>();
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _refreshTokenRepository = _unitOfWork.GetRepository<IRefreshTokenRepository>();
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _studentRepository = _unitOfWork.GetRepository<IStudentRepository>();
            _teacherRepository = _unitOfWork.GetRepository<ITeacherRepository>();
        }

        public async Task<GenericResponseModel<TokenResponseDto>> LoginAsync(LoginUserDto loginUserDto)
        {
            User user = await _appUserRepository.GetUserByUSerNameAsync(loginUserDto.UserName);

            if (user == null)
            {
                return new GenericResponseModel<TokenResponseDto>
                {
                    Data = null,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found"
                };
            }

            bool isSuccess = _passwordHasher.VerifyPassword(loginUserDto.Password, user.Password);

            if (!isSuccess)
            {
                return new GenericResponseModel<TokenResponseDto>
                {
                    Data = null,
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "Wrong password"
                };
            }

            TokenResponseDto tokenResponseDto = _tokenService.GetToken(user);
            int refreshTokenExpirationDays = _configuration.GetValue<int>("JWT:RefreshTokenExpirationDays");

            RefreshToken existingRefreshToken = await _refreshTokenRepository.GetByUserIdAsync(user.Id);
            RefreshToken refreshToken;

            if (existingRefreshToken != null)
            {
                existingRefreshToken.Token = _tokenService.GenerateRefreshToken();
                existingRefreshToken.ExpirationDate = DateTime.Now.AddDays(refreshTokenExpirationDays);

                _refreshTokenRepository.Update(existingRefreshToken);
                refreshToken = existingRefreshToken;
            }
            else
            {
                refreshToken = new RefreshToken
                {
                    Token = _tokenService.GenerateRefreshToken(),
                    ExpirationDate = DateTime.Now.AddDays(refreshTokenExpirationDays),
                    UserId = user.Id
                };

                await _refreshTokenRepository.AddAsync(refreshToken);
            }

            await _unitOfWork.CommitAsync();

            tokenResponseDto.RefreshToken = refreshToken.Token;
            tokenResponseDto.RefreshTokenExpirationDate = refreshToken.ExpirationDate;

            return new GenericResponseModel<TokenResponseDto>
            {
                Data = tokenResponseDto,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Login successful"
            };
        }

        public async Task<GenericResponseModel<TokenResponseDto>> RefreshAsync(TokenApiDto tokenApiDto)
        {
            if (tokenApiDto == null)
            {
                return new GenericResponseModel<TokenResponseDto>
                {
                    Data = null,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid token request"
                };
            }

            string accessToken = tokenApiDto.AccessToken;
            string refreshToken = tokenApiDto.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            string userName = principal.Identity.Name;

            var user = await _appUserRepository.GetUserByUSerNameAsync(userName);
            if (user == null)
            {
                return new GenericResponseModel<TokenResponseDto>
                {
                    Data = null,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found"
                };
            }

            RefreshToken refreshTokenObj = await _refreshTokenRepository.GetRefreshTokenAsync(userName);
            if (refreshTokenObj == null || refreshTokenObj.ExpirationDate <= DateTime.Now || refreshTokenObj.User == null)
            {
                return new GenericResponseModel<TokenResponseDto>
                {
                    Data = null,
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "Refresh token invalid or expired"
                };
            }

            if (refreshTokenObj.Token != refreshToken)
            {
                return new GenericResponseModel<TokenResponseDto>
                {
                    Data = null,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Refresh token mismatch"
                };
            }

            int refreshTokenExpirationDays = _configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays");
            TokenResponseDto tokenResponseDto = _tokenService.GetToken(user);
            string newRefreshToken = _tokenService.GenerateRefreshToken();

            refreshTokenObj.Token = newRefreshToken;
            refreshTokenObj.ExpirationDate = DateTime.Now.AddDays(refreshTokenExpirationDays);

            await _unitOfWork.CommitAsync();

            tokenResponseDto.RefreshToken = refreshTokenObj.Token;
            tokenResponseDto.RefreshTokenExpirationDate = refreshTokenObj.ExpirationDate;

            return new GenericResponseModel<TokenResponseDto>
            {
                Data = tokenResponseDto,
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Token refreshed successfully"
            };
        }

        public async Task RegisterStudentAsync(RegisterStudentUserDto registerStudentUser)
        {
            await AddUserAsync(registerStudentUser.RegisterUserDto, RoleEnum.Student);

            Student student = new()
            {
                Name = registerStudentUser.PostStudentDto.Name,
            };

            await _studentRepository.AddAsync(student);
            await _unitOfWork.CommitAsync();
        }

        public async Task RegisterTeacherAsync(RegisterTeacherUserDto registerTeacherUserDto)
        {
            await AddUserAsync(registerTeacherUserDto.RegisterUserDto, RoleEnum.Teacher);

            Teacher teacher = new()
            {
                Name = registerTeacherUserDto.PostTeacherDto.Name,
            };

            await _teacherRepository.AddAsync(teacher);
            await _unitOfWork.CommitAsync();
        }

        public async Task<GenericResponseModel<string>> RevokeAsync()
        {
            string userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return new GenericResponseModel<string>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "User not authenticated"
                };
            }

            var user = await _appUserRepository.GetUserByUSerNameAsync(userName);
            if (user == null)
            {
                return new GenericResponseModel<string>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found"
                };
            }

            var refreshTokenObj = await _refreshTokenRepository.GetRefreshTokenAsync(userName);
            if (refreshTokenObj == null)
            {
                return new GenericResponseModel<string>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "No refresh token found"
                };
            }

            await _refreshTokenRepository.RemoveAsync(refreshTokenObj);
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<string>
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "Refresh token revoked"
            };
        }

        private async Task<GenericResponseModel<string>> AddUserAsync(RegisterUserDto registerUserDto, RoleEnum roleEnum)
        {
            var existingUser = await _appUserRepository.GetUserByUSerNameAsync(registerUserDto.UserName);
            if (existingUser != null)
            {
                return new GenericResponseModel<string>
                {
                    StatusCode = (int)HttpStatusCode.Conflict,
                    Message = "User already exists"
                };
            }

            User appUser = new()
            {
                UserName = registerUserDto.UserName,
                Email = registerUserDto.Email,
                Password = _passwordHasher.HashPassword(registerUserDto.Password),
                Phone = registerUserDto.Phone,
            };

            appUser.UserRoles = new List<UserRole>()
            {
                new()
                {
                    User = appUser,
                    RoleID = (int)roleEnum,
                }
            };

            await _appUserRepository.AddAsync(appUser);
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<string>
            {
                Data = appUser.UserName,
                StatusCode = (int)HttpStatusCode.Created,
                Message = "User registered successfully"
            };
        }
    }
}
