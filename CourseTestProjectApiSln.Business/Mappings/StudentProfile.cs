using AutoMapper;
using CourseTestProjectApiSln.Business.DTOs.Student;
using CourseTestProjectApiSln.Business.DTOs.Teacher;
using CourseTestProjectApiSln.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.Business.Mappings
{
    public class StudentProfile:Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, GetStudentDto>().ReverseMap();
            CreateMap<Student, PostStudentDto>().ReverseMap();
            CreateMap<Student, UpdateStudentDto>().ReverseMap();

        }

    }


}
