using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using CourseTestProjectApiSln.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using WebApplicationCourseNTier.API.Configs;
using CourseTestProjectApiSln.Business.Extensions;
using CourseTestProjectApiSln.DataAccess.Extensions;
using CourseTestProjectApiSln.Business.Services.Implementations.Infra;
using CourseTestProjectApiSln.API.Extensions;
using FluentValidation.AspNetCore;
using CourseTestProjectApiSln.Business.Validators;
using CourseTestProjectApiSln.Extensions;
using FluentValidation;


namespace CourseTestProjectApiSln
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddControllers(opts => opts.Conventions
                .Add(new RouteTokenTransformerConvention(new ToKebabParameterTransformer())));


            builder.Services.UseCustomValidationResponse();


            builder.Services.AddApplicationLayers(builder.Configuration);

            builder.Services.AddHttpContextAccessor();
        
            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerDocumentation();

          
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
       
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}















//builder.Services.AddControllers()
//         .AddFluentValidation(config =>
//         {
//             config.RegisterValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();
//             config.RegisterValidatorsFromAssemblyContaining<LoginUserDtoValidator>();
//         });
