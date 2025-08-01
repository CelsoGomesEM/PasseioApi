using Elmah.Io.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Passeio.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfig(this IServiceCollection services)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "6330034ca19441d697b25ab8b93a3017";
                o.LogId = new Guid("b9178729-4427-4c36-92b9-5130c93065e7");
            });

            services.AddLogging(builder =>
            {
                builder.AddElmahIo(o =>
                {
                    o.ApiKey = "6330034ca19441d697b25ab8b93a3017";
                    o.LogId = new Guid("b9178729-4427-4c36-92b9-5130c93065e7");
                });
                builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            });

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}
