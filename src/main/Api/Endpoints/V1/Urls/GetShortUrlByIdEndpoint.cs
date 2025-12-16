using JacksonVeroneze.NET.Result;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Endpoints.Extensions;
using UrlShortener.Application.v1.Urls.GetById;

namespace UrlShortener.Api.Endpoints.V1.Urls;

internal static class GetShortUrlByIdEndpoint
{
    public static RouteGroupBuilder AddGetById(
        this RouteGroupBuilder builder)
    {
        builder.MapGet(string.Empty, async (
                [FromServices] IGetByIdShortUrlUseCase useCase,
                [FromQuery(Name = "id")] string code,
                CancellationToken cancellationToken) =>
            {
                GetByIdShortUrlInput input = new(code);

                Result<GetByIdShortUrlOutput> output =
                    await useCase.ExecuteAsync(input, cancellationToken);

                return output.ToIResult();
            })
            .WithName(RouteNames.GetShortUrlById)
            .Produces<GetByIdShortUrlOutput>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
