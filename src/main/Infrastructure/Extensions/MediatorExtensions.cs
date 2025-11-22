using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application;

namespace UrlShortener.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MediatorExtensions
{
    public static IServiceCollection AddMediator(
        this IServiceCollection services)
    {
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssembly(
                typeof(AssemblyReference).Assembly));

        return services;
    }
}
