using CourseTestProjectApiSln.DataAccess.Data;
using CourseTestProjectApiSln.DataAccess.Entities;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstractions;
using CourseTestProjectApiSln.DataAccess.Repositories.Implementations.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.DataAccess.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApiCourseSystem _context;

        public UserRepository(ApiCourseSystem context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUSerNameAsync(string userName)
        {
            return await _context.User
                .Include(x => x.UserRoles).
                 AsNoTracking().
                 SingleOrDefaultAsync(u => u.UserName == userName);
        }
     

    }
}
