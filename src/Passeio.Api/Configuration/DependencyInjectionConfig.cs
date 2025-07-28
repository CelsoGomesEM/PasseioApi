using Passeio.Data.Context;
using Passeio.Data.Repository;
using Passeio.Negocio.Interfaces;

namespace Passeio.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            services.AddScoped<ApiDBContext>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            return services;
        }
    }
}
