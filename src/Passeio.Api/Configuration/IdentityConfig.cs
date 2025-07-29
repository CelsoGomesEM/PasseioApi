using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Passeio.Api.Data;
using Passeio.Data.Context;
using Passeio.Data.Repository;
using Passeio.Negocio.Interfaces;
using Passeio.Negocio.Notificacoes;
using Passeio.Negocio.Services;

namespace Passeio.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDBContext>()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}
