using JacksonVeroneze.NET.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Infrastructure.Configurations;
using UrlShortener.Infrastructure.Contexts;

namespace UrlShortener.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    private const int DefaultCommandTimeout = 5;

    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);
        ArgumentNullException.ThrowIfNull(appConfiguration.Database);

        services.AddRepository()
            .InternalAddDatabase<DefaultDbContext>(
                appConfiguration.Database.WriteConnectionString!)
            .InternalAddDatabase<DefaultReadDbContext>(
                appConfiguration.Database.ReadConnectionString!,
                QueryTrackingBehavior.NoTracking);

        return services;
    }

    private static IServiceCollection InternalAddDatabase<TContext>(
        this IServiceCollection services,
        string connectionString,
        QueryTrackingBehavior behavior = QueryTrackingBehavior.TrackAll)
        where TContext : DbContext
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        services.AddDbContext<TContext>((_, options) =>
            options.UseNpgsql(connectionString, conf =>
                {
                    conf.UseNetTopologySuite();

                    conf.EnableRetryOnFailure()
                        .CommandTimeout(DefaultCommandTimeout);
                })
                .UseQueryTrackingBehavior(behavior)
                .ConfigureOptionsDatabase());

        return services;
    }


    private static void ConfigureOptionsDatabase(
        this DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .EnableThreadSafetyChecks()
            .UseSnakeCaseNamingConvention();
    }
}
