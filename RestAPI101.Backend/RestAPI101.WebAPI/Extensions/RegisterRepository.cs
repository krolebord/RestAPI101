using Microsoft.Extensions.DependencyInjection;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.Services;

namespace RestAPI101.WebAPI.Extensions
{
    public static class RegisterRepository
    {
        public static IServiceCollection AddRepository<TEntity, TImplementation>(this IServiceCollection services)
            where TEntity : class, IEntity
            where TImplementation : class, IRepository<TEntity>
        {
            services.AddTransient<IReadOnlyRepository<TEntity>>(provider => provider.GetService<IRepository<TEntity>>()!);

            services.AddTransient<IRepository<TEntity>, TImplementation>();

            return services;
        }

        public static IServiceCollection AddRepository<TEntity, TInterface, TImplementation>(this IServiceCollection services)
            where TEntity : class, IEntity
            where TInterface : class, IRepository<TEntity>
            where TImplementation : class, TInterface
        {
            services.AddTransient<IReadOnlyRepository<TEntity>>(provider => provider.GetService<TInterface>()!);

            services.AddTransient<IRepository<TEntity>>(provider => provider.GetService<TInterface>()!);

            services.AddTransient<TInterface, TImplementation>();

            return services;
        }
    }
}
