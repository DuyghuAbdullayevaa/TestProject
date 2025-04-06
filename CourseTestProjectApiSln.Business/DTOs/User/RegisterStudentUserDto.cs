using CourseTestProjectApiSln.Business.DTOs.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.Business.DTOs.User
{
    public class RegisterStudentUserDto
    {
        public RegisterUserDto RegisterUserDto { get; set; }    
        public PostStudentDto PostStudentDto { get; set; }  
    }
}
