using CourseTestProjectApiSln.DataAccess.Data;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstactions;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstractions;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstractions.Base;
using CourseTestProjectApiSln.DataAccess.Repositories.Implementations;
using CourseTestProjectApiSln.DataAccess.Repositories.Implementations.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationCourseNTier.DataAccess.Repositories.Implementations;



namespace CourseTestProjectApiSln.DataAccess.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDataAccessLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {

            serviceCollection.AddDbContext<ApiCourseSystem>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            serviceCollection.AddScoped<ITeacherRepository, TeacherRepository>();
            serviceCollection.AddScoped<IStudentRepository, StudentRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();



            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
