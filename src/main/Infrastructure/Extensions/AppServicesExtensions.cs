using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Application.Common.Interfaces.Repositories;
using UrlShortener.Application.v1.Drivers.GetById;
using UrlShortener.Domain.Repositories;
using UrlShortener.Infrastructure.Mapper;
using UrlShortener.Infrastructure.Repositories.Drivers;
using UrlShortener.Infrastructure.System;
using UrlShortener.Infrastructure.UnitOfWork;

namespace UrlShortener.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        // Common
        services.AddScoped<IDataMapper, AutoMapperAdapter>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        
        // UseCases
        services.AddScoped<IGetDriverByIdQueryHandler, GetDriverByIdQueryHandler>();

        // Repositories
        services.AddScoped<IDriverReadRepository, DriverReadRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        
        return services;
    }
}
