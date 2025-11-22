using UrlShortener.Api.Endpoints.Extensions;

namespace UrlShortener.Api.Endpoints.v1.Drivers;

internal static class DriversEndpoints
{
    private const string Resource = "drivers";
    private const int Version = 1;

    public static WebApplication AddDriversEndpoints(
        this WebApplication app)
    {
        RouteGroupBuilder builder = RouteGroupBuilderFactory
            .Factory(app, Resource, Version);

        builder.AddCreate();

        return app;
    }
}
