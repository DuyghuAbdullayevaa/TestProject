using CourseTestProjectApiSln.API.Filters;
using CourseTestProjectApiSln.Business.DTOs.Teacher;
using CourseTestProjectApiSln.Business.Services.Abstractions;
using CourseTestProjectApiSln.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.API.Controllers
{
    [Authorize]
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("{id}")]
        [ApiAuthorization(RoleEnum.Admin, RoleEnum.Teacher)]
        public async Task<ActionResult> GetTeacherByIdAsync(int id) =>
            CreateResponse(await _teacherService.GetTeacherByIdAsync(id));

        [HttpGet]
        [ApiAuthorization(RoleEnum.Admin)]
        public async Task<ActionResult> GetAllTeachersAsync() =>
            CreateResponse(await _teacherService.GetAllTeachersAsync());

        [HttpPost]
        [ApiAuthorization(RoleEnum.Admin)]
        public async Task<ActionResult> CreateTeacherAsync(PostTeacherDto teacherDto) =>
            CreateResponse(await _teacherService.CreateTeacherAsync(teacherDto));

        [HttpPut("{id}")]
        [ApiAuthorization(RoleEnum.Admin)]
        public async Task<ActionResult> UpdateTeacherAsync(int id, UpdateTeacherDto teacherDto) =>
            CreateResponse(await _teacherService.UpdateTeacherAsync(id, teacherDto));

        [HttpDelete("{id}")]
        [ApiAuthorization(RoleEnum.Admin)]
        public async Task<ActionResult> DeleteTeacherAsync(int id) =>
            CreateResponse(await _teacherService.DeleteTeacherAsync(id));
    }
}
