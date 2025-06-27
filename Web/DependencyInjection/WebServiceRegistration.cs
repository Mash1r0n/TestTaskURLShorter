using Infrastructure.DependencyInjection;
using Application.DependencyInjection;

namespace Web.DependencyInjection
{
    public static class WebServiceRegistration
    {
        public static IServiceCollection AddWebAppServiceRegistration(this IServiceCollection services, string connectionString)
        {
            services.AddInfrastructureServices(connectionString);
            services.AddApplicationServices();

            return services;
        }
    }
}
