using CourseTestProjectApiSln.API.Filters;
using CourseTestProjectApiSln.Business.DTOs.User;
using CourseTestProjectApiSln.Business.Services.Abstractions;
using CourseTestProjectApiSln.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseTestProjectApiSln.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiAuthorization(RoleEnum.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterTeacher(RegisterTeacherUserDto registerTeacherUserDto)
        {
            await _userService.RegisterTeacherAsync(registerTeacherUserDto);
            return Ok();


        }
    }
}
