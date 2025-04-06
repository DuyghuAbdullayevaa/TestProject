using CourseTestProjectApiSln.Business.DTOs.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.Business.DTOs.User
{
    public class RegisterTeacherUserDto
    {
        public RegisterUserDto  RegisterUserDto { get; set; }  
        public PostTeacherDto PostTeacherDto { get; set; }
    }
}
