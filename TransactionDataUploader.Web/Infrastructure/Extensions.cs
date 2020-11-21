using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionDataUploader.Core.Infrastructure.Persistence;

namespace TransactionDataUploader.Web.Infrastructure
{
    public static class  CoreServiceRegistration
    {
        public static IServiceCollection RegisterDbAccess(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<TransactionContext>(options => options.UseSqlServer(
                config.GetConnectionString("DefaultConnection"),
                b =>
                {
                    b.MigrationsAssembly(typeof(Startup).Assembly.FullName);
                }));
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            return services;
        }
    }
}
