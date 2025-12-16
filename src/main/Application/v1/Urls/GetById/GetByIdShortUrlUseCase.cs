using JacksonVeroneze.NET.Result;
using UrlShortener.Application.Abstractions.Repositories;
using UrlShortener.Application.Common.Parameters;
using UrlShortener.Application.v1.Urls.Common.Models;
using UrlShortener.Domain.Aggregates.Url;
using UrlShortener.Domain.Core.Errors;

namespace UrlShortener.Application.v1.Urls.GetById;

public sealed class GetByIdShortUrlUseCase(
    IShortUrlReadRepository shortUrlReadRepository,
    UrlShortenerParameters parameters) : IGetByIdShortUrlUseCase

{
    public async Task<Result<GetByIdShortUrlOutput>> ExecuteAsync(
        GetByIdShortUrlInput request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        ShortUrl? shortUrl = await shortUrlReadRepository
            .GetByCodeAsync(request.Code!, cancellationToken);

        if (shortUrl is null)
        {
            return Result<GetByIdShortUrlOutput>.FromNotFound(
                DomainErrors.ShortUrlErrors.NotFound);
        }

        Uri uri = ComposeShortenerUrl(shortUrl.Code);

        GetByIdShortUrlOutput result =
            ComposeOutput(shortUrl.Code, uri, shortUrl);

        return Result<GetByIdShortUrlOutput>.WithSuccess(result);
    }

    private Uri ComposeShortenerUrl(string code)
    {
        UriBuilder uriBuilder = new()
        {
            Scheme = parameters.Scheme!,
            Host = parameters.BaseDomain!,
            Query = $"{parameters.QueryStringName}={code}",
        };

        return uriBuilder.Uri;
    }

    private static GetByIdShortUrlOutput ComposeOutput(
        string code, Uri uri, ShortUrl shortUrl)
    {
        GetByIdShortUrlOutput result = new()
        {
            Data = new ShortUrlOutput
            {
                Code = code,
                CreationDate = DateTimeOffset.UtcNow,
                ExpirationDate = shortUrl.ExpiresAt,
                ShortenedUrl = uri,
            },
        };

        return result;
    }
}
