﻿using Passeio.Api.Extensions;
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
            services.AddScoped<ILugarRepository, LugarRepository>();
            services.AddScoped<ILugarService, LugarService>();
            services.AddScoped<INotificador, Notificador>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}
