using CourseTestProjectApiSln.API.Filters;
using CourseTestProjectApiSln.Business.DTOs.Student;
using CourseTestProjectApiSln.Business.Services.Abstractions;
using CourseTestProjectApiSln.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.API.Controllers
{
    
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("{id}")]
        [ApiAuthorization(RoleEnum.Admin,RoleEnum.Student,RoleEnum.Teacher)]
        public async Task<ActionResult> GetStudentByIdAsync(int id) =>
            CreateResponse(await _studentService.GetStudentByIdAsync(id));

        [HttpGet]
        [ApiAuthorization(RoleEnum.Admin,RoleEnum.Teacher)]
        public async Task<ActionResult> GetAllStudentsAsync() =>
           CreateResponse(await _studentService.GetAllStudentsAsync());

        [HttpPost]
        [ApiAuthorization(RoleEnum.Admin)]
        public async Task<ActionResult> CreateStudentAsync(PostStudentDto studentDto) =>
            CreateResponse(await _studentService.CreateStudentAsync(studentDto));

        [HttpPut("{id}")]
        [ApiAuthorization(RoleEnum.Admin)]
        public async Task<ActionResult> UpdateStudentAsync(int id, UpdateStudentDto studentDto) =>
            CreateResponse(await _studentService.UpdateStudentAsync(id, studentDto));

        [HttpDelete("{id}")]
        [ApiAuthorization(RoleEnum.Admin)]
        public async Task<ActionResult> DeleteStudentAsync(int id) =>
            CreateResponse(await _studentService.DeleteStudentAsync(id));
    }
}
