using CourseTestProjectApiSln.DataAccess.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace CourseTestProjectApiSln.DataAccess.Repositories.Abstractions.Base
{
    public interface IRepository<TEntity> : IRepositoryBase
              where TEntity : class, IEntity
    {
        Task<bool> AddAsync(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> RemoveAsync(TEntity entity);
        Task<bool> RemoveByIdAsync(int id);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);
        //Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);
    }
}


