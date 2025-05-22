using Antonava__353504.Persistense.Data;
using Antonava__353504.Persistense.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Antonava__353504.Persistense
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork, FakeUnitOfWork>();
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, DbContextOptions options)
        {
            services.AddSingleton<AppDbContext>(new AppDbContext((DbContextOptions<AppDbContext>)options));
            services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
            return services;
        }
    }
}
