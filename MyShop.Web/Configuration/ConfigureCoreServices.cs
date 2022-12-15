using MyShop.ApplicationCore.Interfaces;
using MyShop.Infrastructure;

namespace MyShop.Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services) 
        {
            return services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>)); 
        } 
    }
}
