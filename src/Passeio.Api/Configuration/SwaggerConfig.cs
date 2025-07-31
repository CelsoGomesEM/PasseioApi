using Microsoft.OpenApi.Models;

namespace Passeio.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Define o documento Swagger com uma única versão
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API - Passeios",
                    Version = "v1",
                    Description = "Api disponibilizada para nosso site de informações, avaliações sobre passeios e locais para viagem.",
                    Contact = new OpenApiContact
                    {
                        Name = "Celso Gomes",
                        Email = "celsogomes22@gmail.com"
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }

    }
}
