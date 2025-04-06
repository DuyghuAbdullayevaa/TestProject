using CourseTestProjectApiSln.Business.DTOs.BaseResponseModel;
using CourseTestProjectApiSln.Business.DTOs.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.Business.Services.Abstractions
{
    public interface ITeacherService
    {
        Task<GenericResponseModel<GetTeacherDto>> GetTeacherByIdAsync(int id);
        Task<GenericResponseModel<PostTeacherDto>> CreateTeacherAsync(PostTeacherDto teacherDto);
        Task<GenericResponseModel<bool>> UpdateTeacherAsync(int id, UpdateTeacherDto teacherDto);
        Task<GenericResponseModel<bool>> DeleteTeacherAsync(int id);
        Task<GenericResponseModel<IEnumerable<GetTeacherDto>>> GetAllTeachersAsync();
    }
}
