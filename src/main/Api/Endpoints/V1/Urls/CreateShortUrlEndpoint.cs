using JacksonVeroneze.NET.Result;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Endpoints.Extensions;
using UrlShortener.Application.v1.Urls.Create;

namespace UrlShortener.Api.Endpoints.V1.Urls;

internal static class CreateShortUrlEndpoint
{
    public static RouteGroupBuilder AddCreate(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ICreateShortUrlUseCase useCase,
                [FromServices] LinkGenerator linkGenerator,
                [FromBody] CreateShortUrlInput input,
                HttpContext httpContext,
                CancellationToken cancellationToken) =>
            {
                Result<CreateShortUrlOutput> output =
                    await useCase.ExecuteAsync(input, cancellationToken);

                return output.ToCreatedResultFromRoute(
                    linkGenerator,
                    httpContext,
                    RouteNames.GetShortUrlById,
                    output.Value?.Data?.Code!
                );
            })
            .Produces<CreateShortUrlOutput>(
                statusCode: StatusCodes.Status201Created)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
