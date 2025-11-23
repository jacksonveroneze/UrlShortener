using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Abstractions.Services;
using UrlShortener.Application.Abstractions.Uow;
using UrlShortener.Application.v1.Urls.Create;
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
        services.AddScoped<IDataMapper, MapsterAdapter>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IShortCodeGenerator, ShortCodeGenerator>();
        services.AddScoped<IUrlSanitizer, UrlSanitizer>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        
        // UseCases
        services.AddScoped<ICreateShortUrlUseCase, CreateShortUrlUseCase>();

        // Repositories
        services.AddScoped<IShortUrlRepository, ShortUrlRepository>();
        
        return services;
    }
}
