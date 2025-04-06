using AutoMapper;
using CourseTestProjectApiSln.Business.DTOs.Teacher;
using CourseTestProjectApiSln.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.Business.Mappings
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, GetTeacherDto>().ReverseMap();
            CreateMap<Teacher, PostTeacherDto>().ReverseMap();
            CreateMap<Teacher, UpdateTeacherDto>().ReverseMap();

        }

    }
}
