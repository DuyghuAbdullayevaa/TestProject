using AutoMapper;
using CourseTestProjectApiSln.Business.DTOs.BaseResponseModel;
using CourseTestProjectApiSln.Business.DTOs.Teacher;
using CourseTestProjectApiSln.Business.Services.Abstractions;
using CourseTestProjectApiSln.DataAccess.Entities;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstactions;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstractions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.Business.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepo;

        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _teacherRepo = _unitOfWork.GetRepository<ITeacherRepository>();
        }

        public async Task<GenericResponseModel<PostTeacherDto>> CreateTeacherAsync(PostTeacherDto teacherDto)
        {

            if (teacherDto == null)
            {
                return new GenericResponseModel<PostTeacherDto>
                {
                    StatusCode = 400, // Bad Request
                    Data = null
                };
            }

            // 2. Yeni müəllimi əlavə etmək
            var teacher = _mapper.Map<Teacher>(teacherDto); // DTO-nu Teacher entity-sinə çeviririk
            await _teacherRepo.AddAsync(teacher); // Repository-yə əlavə edirik
            await _unitOfWork.CommitAsync(); // Dəyişiklikləri saxlamaq

            // 3. Yaradılmış müəllimi DTO-ya çeviririk
            var createdTeacherDto = _mapper.Map<PostTeacherDto>(teacher);

            return new GenericResponseModel<PostTeacherDto>
            {
                StatusCode = 201, // Created
                Data = createdTeacherDto
            };
        }


        // Method to delete a teacher by ID
        public async Task<GenericResponseModel<bool>> DeleteTeacherAsync(int id)
        {
            var teacher = await _teacherRepo.GetByIdAsync(id);

            if (teacher == null)
            {
                return new GenericResponseModel<bool>
                {
                    StatusCode = 404, 
                    Data = false
                };
            }

            await _teacherRepo.RemoveAsync(teacher);
            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<bool>
            {
                StatusCode = 200, 
                Data = true
            };
        }


        public async Task<GenericResponseModel<IEnumerable<GetTeacherDto>>> GetAllTeachersAsync()
        {
            var teachers = await _teacherRepo.GetAllAsync(null); 
            if (teachers == null || !teachers.Any())
            {
                return new GenericResponseModel<IEnumerable<GetTeacherDto>>
                {
                    StatusCode = 404, 
                    Data = Enumerable.Empty<GetTeacherDto>()
                };
            }

            var teacherDtos = _mapper.Map<IEnumerable<GetTeacherDto>>(teachers);
            return new GenericResponseModel<IEnumerable<GetTeacherDto>>
            {
                StatusCode = 200,
                Data = teacherDtos
            };
        }

      
        public async Task<GenericResponseModel<GetTeacherDto>> GetTeacherByIdAsync(int id)
        {
         
            var teacher = await _teacherRepo.GetAllAsync(t => t.Id == id && !t.IsDeleted);

           
            if (teacher == null || !teacher.Any())
            {
                return new GenericResponseModel<GetTeacherDto>
                {
                    StatusCode = 404, 
                    Data = null
                };
            }

            
            var teacherDto = _mapper.Map<GetTeacherDto>(teacher.FirstOrDefault());

            return new GenericResponseModel<GetTeacherDto>
            {
                StatusCode = 200, 
                Data = teacherDto
            };
        }


        public async Task<GenericResponseModel<bool>> UpdateTeacherAsync(int id, UpdateTeacherDto teacherDto)
        {
            
            var teacher = await _teacherRepo.GetByIdAsync(id);

            
            if (teacher == null || teacher.IsDeleted)
            {
                return new GenericResponseModel<bool>
                {
                    StatusCode = 404, 
                    Data = false
                };
            }

            _mapper.Map(teacherDto, teacher);

            await _teacherRepo.Update(teacher);

            await _unitOfWork.CommitAsync();

            return new GenericResponseModel<bool>
            {
                StatusCode = 200, 
                Data = true
            };
        }
    }
}
