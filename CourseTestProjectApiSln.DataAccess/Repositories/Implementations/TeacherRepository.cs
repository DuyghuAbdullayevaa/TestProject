using CourseTestProjectApiSln.DataAccess.Data;
using CourseTestProjectApiSln.DataAccess.Entities;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstactions;
using CourseTestProjectApiSln.DataAccess.Repositories.Implementations.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.DataAccess.Repositories.Implementations
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        private readonly ApiCourseSystem _courseSystemApi;
        public TeacherRepository(ApiCourseSystem context) : base(context)
        {
            _courseSystemApi = context;
        }


    }
}
