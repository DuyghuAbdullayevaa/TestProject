
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
//using CourseSystem.Services.Implementations;
using System.Collections;
using CourseTestProjectApiSln.Business.Services.Abstractions;

using CourseTestProjectApiSln.Business.Services.Abstractions.Infra;
using CourseTestProjectApiSln.Business.Services.Implementations.Infra;
using CourseTestProjectApiSln.Business.Services.Implementations;


namespace CourseTestProjectApiSln.Business.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IFileService, FileService>();
            services.AddScoped<IStudentService, StudentService>();
            //services.AddScoped<IGroupService, GroupService>();
            //services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ITeacherService, TeacherService>();
            //services.AddScoped<ILessonService, LessonService>();
            //services.AddScoped<ITopicService, TopicService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        }
    }
}
