using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using UrlShortener.Infrastructure.Configurations;

namespace UrlShortener.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class RedisExtensions
{
    public static IServiceCollection AddRedisConnectionMultiplexer(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        ConfigurationOptions configurationOptions = new()
        {
            Ssl = false,
            AbortOnConnectFail = false,
            EndPoints = { appConfiguration.Cache!.Endpoint! },
            ClientName = $"{appConfiguration.AppName}-{Guid.NewGuid()}",
        };

        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(configurationOptions));

        return services;
    }
}
