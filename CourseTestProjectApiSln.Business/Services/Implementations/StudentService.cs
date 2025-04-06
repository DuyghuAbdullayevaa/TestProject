using AutoMapper;
using CourseTestProjectApiSln.Business.DTOs.BaseResponseModel;
using CourseTestProjectApiSln.Business.DTOs.Student;
using CourseTestProjectApiSln.Business.DTOs.Teacher;
using CourseTestProjectApiSln.Business.Services.Abstractions;
using CourseTestProjectApiSln.DataAccess.Entities;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstactions;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstractions.Base;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.Business.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRep;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _studentRep = _unitOfWork.GetRepository<IStudentRepository>();
        }

        public async Task<GenericResponseModel<PostStudentDto>> CreateStudentAsync(PostStudentDto studentDto)
        {
            if (studentDto == null)
            {
                return new GenericResponseModel<PostStudentDto>
                {
                    StatusCode = 400,
                    Message = "Bad request",
                    Data = null
                };
            }

            var student = _mapper.Map<Student>(studentDto);
            await _studentRep.AddAsync(student);
            await _unitOfWork.CommitAsync();

            var createdStudentDto = _mapper.Map<PostStudentDto>(student);

            return new GenericResponseModel<PostStudentDto>
            {
                StatusCode = 201,
                Message = "Created successfully",
                Data = studentDto
            };
        }

        public async Task<GenericResponseModel<bool>> DeleteStudentAsync(int id)
        {
            var student = await _studentRep.GetByIdAsync(id);

            if (student == null)
            {
                return new GenericResponseModel<bool>
                {
                    StatusCode = 404,
                    Message = "Student not found",
                    Data = false
                };
            }

            await _studentRep.RemoveAsync(student);
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<bool>
            {
                StatusCode = 200,
                Message = "Student deleted successfully",
                Data = true
            };
        }

        public async Task<GenericResponseModel<IEnumerable<GetStudentDto>>> GetAllStudentsAsync()
        {
            var students = await _studentRep.GetAllAsync(null);

            if (students == null || !students.Any())
            {
                return new GenericResponseModel<IEnumerable<GetStudentDto>>
                {
                    StatusCode = 404,
                    Message = "No students found",
                    Data = Enumerable.Empty<GetStudentDto>()
                };
            }

            var studentDtos = _mapper.Map<IEnumerable<GetStudentDto>>(students);
            return new GenericResponseModel<IEnumerable<GetStudentDto>>
            {
                StatusCode = 200,
                Message = "Students retrieved successfully",
                Data = studentDtos
            };
        }

        public async Task<GenericResponseModel<GetStudentDto>> GetStudentByIdAsync(int id)
        {
            var students = await _studentRep.GetAllAsync(t => t.Id == id && !t.IsDeleted);

            if (students == null || !students.Any())
            {
                return new GenericResponseModel<GetStudentDto>
                {
                    StatusCode = 404,
                    Message = "Student not found",
                    Data = null
                };
            }

            var studentDto = _mapper.Map<GetStudentDto>(students.FirstOrDefault());

            return new GenericResponseModel<GetStudentDto>
            {
                StatusCode = 200,
                Message = "Student retrieved successfully",
                Data = studentDto
            };
        }

        public async Task<GenericResponseModel<bool>> UpdateStudentAsync(int id, UpdateStudentDto studentDto)
        {
            var student = await _studentRep.GetByIdAsync(id);

            if (student == null || student.IsDeleted)
            {
                return new GenericResponseModel<bool>
                {
                    StatusCode = 404,
                    Message = "Student not found",
                    Data = false
                };
            }

            _mapper.Map(studentDto, student);

            await _studentRep.Update(student);
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<bool>
            {
                StatusCode = 200,
                Message = "Student updated successfully",
                Data = true
            };
        }
    }
   
}
