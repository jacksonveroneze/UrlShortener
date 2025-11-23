using Microsoft.AspNetCore.Builder;
using UrlShortener.Infrastructure.Configurations;

namespace UrlShortener.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class LoggingExtensions
{
    public static WebApplicationBuilder AddLogger(
        this WebApplicationBuilder builder,
        AppConfiguration appConfiguration)
    {
        return builder;
    }
}
