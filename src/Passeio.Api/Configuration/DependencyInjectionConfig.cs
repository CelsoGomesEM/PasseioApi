using Passeio.Data.Context;
using Passeio.Data.Repository;
using Passeio.Negocio.Interfaces;
using Passeio.Negocio.Notificacoes;
using Passeio.Negocio.Services;

namespace Passeio.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services)
        {
            services.AddScoped<ApiDBContext>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<INotificador, Notificador>();
            return services;
        }
    }
}
