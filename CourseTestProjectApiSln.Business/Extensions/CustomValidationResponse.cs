
using CourseTestProjectApiSln.Business.DTOs.BaseResponseModel;
using CourseTestProjectApiSln.Business.DTOs.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using System.Linq;

namespace CourseTestProjectApiSln.Extensions
{
    public static class CustomValidationResponse
    {
        public static void UseCustomValidationResponse(this IServiceCollection service)
        {
            service.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    
                    var errors = context.ModelState.Values
                        .Where(x => x.Errors.Count > 0)
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    ErrorDto errorResponseDto = new ErrorDto(errors);


                    var response = new GenericResponseModel<ErrorDto>
                    {
                        Data = errorResponseDto,  
                        StatusCode = 400          
                    };

              
                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
