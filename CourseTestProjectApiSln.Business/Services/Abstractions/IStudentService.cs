using CourseTestProjectApiSln.Business.DTOs.BaseResponseModel;
using CourseTestProjectApiSln.Business.DTOs.Student;
using CourseTestProjectApiSln.Business.DTOs.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.Business.Services.Abstractions
{
    public interface IStudentService
    {
        Task<GenericResponseModel<GetStudentDto>> GetStudentByIdAsync(int id);
        Task<GenericResponseModel<PostStudentDto>> CreateStudentAsync(PostStudentDto studentDto);
        Task<GenericResponseModel<bool>> UpdateStudentAsync(int id, UpdateStudentDto studentDto);
        Task<GenericResponseModel<bool>> DeleteStudentAsync(int id);
        Task<GenericResponseModel<IEnumerable<GetStudentDto>>> GetAllStudentsAsync();
    }
}
