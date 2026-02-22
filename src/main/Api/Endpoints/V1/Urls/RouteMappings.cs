using UrlShortener.Api.Endpoints.Extensions;

namespace UrlShortener.Api.Endpoints.V1.Urls;

internal static class RouteMappings
{
    private const string Resource = "urls";
    private const int Version = 1;

    public static WebApplication AddUrlsEndpoints(
        this WebApplication app)
    {
        RouteGroupBuilder builder = RouteGroupBuilderFactory
            .Factory(app, Resource, Version);

        builder.AddCreate()
            .AddGetById();

        return app;
    }
}
