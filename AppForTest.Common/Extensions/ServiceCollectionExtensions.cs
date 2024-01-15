using AppForTest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppForTest.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDb(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppForTestDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureMediatR(IServiceCollection services)
        {
            
        }
    }
}