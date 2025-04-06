using CourseTestProjectApiSln.DataAccess.Data;
using CourseTestProjectApiSln.DataAccess.Entities;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstactions;
using CourseTestProjectApiSln.DataAccess.Repositories.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.DataAccess.Repositories.Implementations
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly ApiCourseSystem _courseSystemApi;
        public StudentRepository(ApiCourseSystem context) : base(context)
        {
            _courseSystemApi = context;
        }


    }
}
