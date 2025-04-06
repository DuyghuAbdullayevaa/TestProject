using CourseTestProjectApiSln.DataAccess.Data;
using CourseTestProjectApiSln.DataAccess.Entities.Base;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstractions.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace CourseTestProjectApiSln.DataAccess.Repositories.Implementations.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ApiCourseSystem _courseNtierDbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApiCourseSystem courseNtierDbContext)
        {
            _courseNtierDbContext = courseNtierDbContext;
            _dbSet = courseNtierDbContext.Set<TEntity>();
        }


        public async Task<bool> AddAsync(TEntity entity)
        {
            if (entity == null)
                return false;

            EntityEntry<TEntity> entityEntry = await _dbSet.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            if (entity == null)
                return false;

            EntityEntry<TEntity> entityEntry = _dbSet.Remove(entity);


            return entityEntry.State == EntityState.Deleted;
        }



        public async Task<bool> RemoveByIdAsync(int id)
        {

            var entity = await GetByIdAsync(id);


            if (entity == null)
                return false;

            return await RemoveAsync(entity);
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null)
            {

                return await _dbSet.Where(x => !((BaseEntity)(object)x).IsDeleted).ToListAsync();
            }
            else
            {

                return await _dbSet.Where(expression).ToListAsync();
            }
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<bool> Update(TEntity entity)
        {
            if (entity == null)
                return false;

            EntityEntry<TEntity> entityEntry = _dbSet.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

    }
}
