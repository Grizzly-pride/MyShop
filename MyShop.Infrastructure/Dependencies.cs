using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyShop.ApplicationCore.Entities;
using MyShop.ApplicationCore.Interfaces;
using MyShop.Infrastructure.Data;


namespace MyShop.Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services) 
        {
            services.AddDbContext<CatalogContext>(context =>
            context.UseSqlServer(configuration.GetConnectionString("CatalogConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
        }
    }
}
