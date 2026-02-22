using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using UrlShortener.Infrastructure.Configurations;
using ZiggyCreatures.Caching.Fusion;

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

        services
            .AddFusionCacheSystemTextJsonSerializer()
            .AddFusionCache(cacheName: "ShortUrl")
            .WithRegisteredSerializer()
            .WithRegisteredDistributedCache()
            .WithOptions(options => { options.DisableTagging = true; })
            .WithCacheKeyPrefix("Url:")
            .WithDefaultEntryOptions(options =>
            {
                options.IsFailSafeEnabled = true;
                options.Duration = TimeSpan.FromMinutes(5);
                options.FailSafeMaxDuration = TimeSpan.FromMinutes(10);
                options.FailSafeThrottleDuration = TimeSpan.FromMinutes(11);

                options.EagerRefreshThreshold = 0.9f;

                options.JitterMaxDuration = TimeSpan.FromSeconds(12);

                options.FactorySoftTimeout = TimeSpan.FromSeconds(13);
                options.FactoryHardTimeout = TimeSpan.FromSeconds(14);
                options.DistributedCacheSoftTimeout = TimeSpan.FromSeconds(15);
                options.DistributedCacheHardTimeout = TimeSpan.FromSeconds(16);

                options.AllowBackgroundDistributedCacheOperations = true;
                options.AllowTimedOutFactoryBackgroundCompletion = true;
            });
        
        return services;
    }
}
