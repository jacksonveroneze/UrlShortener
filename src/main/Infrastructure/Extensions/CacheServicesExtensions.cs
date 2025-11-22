using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using UrlShortener.Infrastructure.Configurations;

namespace UrlShortener.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class CacheServicesExtensions
{
    public static IServiceCollection AddCached(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        if (appConfiguration.Cache?.Type is CacheType.Memory)
        {
            services.AddDistributedMemoryCache();
        }
        else
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName =
                    $"{appConfiguration.AppName}-" +
                    $"{appConfiguration.AppVersion}";

                options.ConfigurationOptions = new ConfigurationOptions
                {
                    Ssl = false,
                    AbortOnConnectFail = false,
                    EndPoints = { appConfiguration.Cache!.Endpoint! },
                    ClientName = $"{appConfiguration.AppName}-{Guid.NewGuid()}",
                };
            });
        }

        return services;
    }
}
