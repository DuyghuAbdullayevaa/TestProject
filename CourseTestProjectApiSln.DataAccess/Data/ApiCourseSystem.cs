using CourseTestProjectApiSln.DataAccess.Entities;
using CourseTestProjectApiSln.DataAccess.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CourseTestProjectApiSln.DataAccess.Data
{
    public class ApiCourseSystem : DbContext
    {
        // DbSet-lər burada müəyyən edilir
        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        // DbContext-in konstruktoru
        public ApiCourseSystem(DbContextOptions<ApiCourseSystem> options)
            : base(options)
        {
        }

        // OnModelCreating metodunda konfiqurasiyalar tətbiq edilir
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        // SaveChanges metodunda "soft delete" və audit sahələri idarə olunur
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>(); // Entity tracking

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    // Yeni əlavə edilmiş obyektlər üçün CreateDate sahəsini təyin et
                    entry.Entity.CreateDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // Dəyişdirilmiş obyektlər üçün UpdateDate sahəsini təyin et
                    entry.Entity.UpdateDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    // Soft delete: obyektin fiziki olaraq silinməsini əngəlləyir
                    if (entry.Entity is BaseEntity baseEntity)
                    {
                        entry.State = EntityState.Modified; // Fiziki silinmə əvəzinə dəyişdirilir
                        baseEntity.IsDeleted = true; // Deleted flag aktiv edilir
                        baseEntity.DeleteDate = DateTime.Now; // DeleteDate təyin edilir
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
