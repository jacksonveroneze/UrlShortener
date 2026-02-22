using UrlShortener.Api.Middlewares;
using UrlShortener.Infrastructure.Configurations;
using UrlShortener.Infrastructure.Extensions;

namespace UrlShortener.Api.Extensions;

internal static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder Configure()
        {
            builder.Host.ConfigureHostOptions(options =>
                options.ShutdownTimeout = TimeSpan.FromSeconds(10));
        
            builder.Configuration
                .AddEnvironmentVariables("APP_CONFIG_");

            builder.Services.AddAppConfigs(builder.Configuration);
        
            ServiceProvider serviceProvider = builder.Services
                .BuildServiceProvider();
        
            AppConfiguration appConfiguration = serviceProvider
                .GetRequiredService<AppConfiguration>();
        
            builder.ConfigureDefaultServices(appConfiguration);
        
            builder.AddLogger(appConfiguration);
        
            MetricsExtensions.AddMetrics();
        
            return builder;
        }

        private WebApplicationBuilder ConfigureDefaultServices(
            AppConfiguration appConfiguration)
        {
            builder.Services
                .AddProblemDetails()
                .AddExceptionHandler<CustomExceptionHandler>()
                .AddJsonOptionsSerialize()
                .AddCorrelation()
                .AddAuthentication(appConfiguration)
                .AddAuthorization(appConfiguration)
                .AddHttpContextAccessor()
                .AddCultureConfiguration()
                .AddAppVersioning()
                .AddRouting()
                .AddApplicationServices()
                .AddMapper()
                .AddCached(appConfiguration)
                .AddRedisConnectionMultiplexer(appConfiguration)
                .AddOpenTelemetry(appConfiguration)
                .AddDatabase(appConfiguration)
                .AddHealthChecks();

            if (!builder.Environment.IsProduction())
            {
                builder.Services
                    .AddOpenApi();
            }

            return builder;
        }
    }
}
