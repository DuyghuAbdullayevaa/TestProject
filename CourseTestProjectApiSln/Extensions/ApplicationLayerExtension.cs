using CourseTestProjectApiSln.DataAccess.Extensions;

namespace CourseTestProjectApiSln.Business.Extensions
{
    public static class ApplicationLayerExtension
    {
        public static IServiceCollection AddApplicationLayers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataAccessLayer(configuration);
            services.AddBusinessLayer();

            return services;
        }
    }
}

