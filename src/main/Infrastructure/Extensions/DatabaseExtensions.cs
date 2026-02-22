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

    extension(IServiceCollection services)
    {
        public IServiceCollection AddDatabase(AppConfiguration appConfiguration)
        {
            ArgumentNullException.ThrowIfNull(appConfiguration);
            ArgumentNullException.ThrowIfNull(appConfiguration.Database);
            ArgumentException.ThrowIfNullOrEmpty(appConfiguration.Database.WriteConnectionString);
            ArgumentException.ThrowIfNullOrEmpty(appConfiguration.Database.ReadConnectionString);

            services.AddRepository()
                .InternalAddDatabase<DefaultDbContext>(
                    appConfiguration.Database.WriteConnectionString!)
                .InternalAddDatabase<DefaultReadDbContext>(
                    appConfiguration.Database.ReadConnectionString!,
                    QueryTrackingBehavior.NoTracking);

            return services;
        }

        private IServiceCollection InternalAddDatabase<TContext>(string connectionString,
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
