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
                [FromBody] CreateShortUrlInput request,
                CancellationToken cancellationToken) =>
            {
                Result<CreateShortUrlOutput> response =
                    await useCase.ExecuteAsync(request, cancellationToken);

                return Results.Created(string.Empty, response.Value);
            })
            .Produces<CreateShortUrlOutput>(
                statusCode: StatusCodes.Status201Created)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
