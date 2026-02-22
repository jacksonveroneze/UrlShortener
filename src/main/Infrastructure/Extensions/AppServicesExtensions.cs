using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Abstractions.Repositories;
using UrlShortener.Application.Abstractions.Services;
using UrlShortener.Application.Abstractions.Uow;
using UrlShortener.Application.Common.Services;
using UrlShortener.Application.v1.Urls.Create;
using UrlShortener.Application.v1.Urls.GetById;
using UrlShortener.Domain.Repositories;
using UrlShortener.Infrastructure.Repositories.ShortUrl;
using UrlShortener.Infrastructure.Services;
using UrlShortener.Infrastructure.UnitOfWork;

namespace UrlShortener.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        // Common
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddScoped<IDataMapper, MapsterAdapter>();

        services.AddScoped<IShortCodeGenerator, ShortCodeGeneratorHashIds>();
        services.AddScoped<IShortCodeService, ShortCodeService>();

        services.AddSingleton<IUrlSanitizer, UrlSanitizer>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IDistributedCounter, RedisDistributedCounter>();

        // UseCases
        services.AddScoped<ICreateShortUrlUseCase, CreateShortUrlUseCase>();
        services.AddScoped<IGetByIdShortUrlUseCase, GetByIdShortUrlUseCase>();

        // Repositories
        services.AddScoped<IShortUrlReadRepository, ShortUrlReadRepository>();
        services.AddScoped<IShortUrlReadCacheRepository, ShortUrlReadCacheRepository>();

        services.AddScoped<IShortUrlRepository, ShortUrlRepository>();

        return services;
    }
}
