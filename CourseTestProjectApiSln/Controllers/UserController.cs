using CourseTestProjectApiSln.Business.DTOs.BaseResponseModel;
using CourseTestProjectApiSln.Business.DTOs.Teacher;
using CourseTestProjectApiSln.Business.DTOs.User;
using CourseTestProjectApiSln.Business.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CourseTestProjectApiSln.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterStudentUserDto RegisterStudentUserDto)
        {
            await _userService.RegisterStudentAsync(RegisterStudentUserDto);
            return Ok();
          

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {

            return Ok(await _userService.LoginAsync(loginUserDto));

        }
        [HttpPost]
        public async Task<IActionResult> Refresh(TokenApiDto tokenApiDto)
        {

            return Ok(await _userService.RefreshAsync(tokenApiDto));

        }
        [HttpPost, Authorize]
     
        public async Task<IActionResult> Revoke()
        {
            return Ok(await _userService.RevokeAsync());
           
        }
    }
}
