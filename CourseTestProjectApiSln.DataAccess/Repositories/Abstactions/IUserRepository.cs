using CourseTestProjectApiSln.DataAccess.Entities;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstractions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CourseTestProjectApiSln.DataAccess.Repositories.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUSerNameAsync(string userName);
    }
}
