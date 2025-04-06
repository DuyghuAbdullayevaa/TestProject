using CourseTestProjectApiSln.Business.DTOs.BaseResponseModel;
using CourseTestProjectApiSln.Business.DTOs.User;
using CourseTestProjectApiSln.DataAccess.Entities;
using CourseTestProjectApiSln.DataAccess.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CourseTestProjectApiSln.Business.Services.Abstractions
{
    public interface IUserService
    {
        Task<GenericResponseModel<string>> RevokeAsync();
        Task<GenericResponseModel<TokenResponseDto>> RefreshAsync(TokenApiDto tokenApiDto);
        Task<GenericResponseModel<TokenResponseDto>> LoginAsync(LoginUserDto loginUserDto);
        Task RegisterStudentAsync(RegisterStudentUserDto registerStudentUserDto);
        Task RegisterTeacherAsync(RegisterTeacherUserDto registerTeacherUserDto);
        //Task<GenericResponseModel<TokenResponseDto>> RefreshAsync(string refreshToken);
        //Task<TokenResponseDto> LoginAsync(LoginUserDto loginUserDto);

    }
}
