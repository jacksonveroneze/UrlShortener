using JacksonVeroneze.NET.Result;
using UrlShortener.Application.Abstractions.Repositories;
using UrlShortener.Application.Abstractions.Services;
using UrlShortener.Application.Abstractions.Uow;
using UrlShortener.Application.Common.Parameters;
using UrlShortener.Domain;
using UrlShortener.Domain.Aggregates.Url;
using UrlShortener.Domain.Core.Errors;
using UrlShortener.Domain.Repositories;

namespace UrlShortener.Application.v1.Urls.Create;

public sealed class CreateShortUrlUseCase(
    IShortUrlReadRepository shortUrlReadRepository,
    IShortUrlRepository urlRepository,
    IUnitOfWork unitOfWork,
    IShortCodeService codeGenerator,
    IDateTimeProvider clock,
    IUrlSanitizer sanitizer,
    UrlShortenerParameters parameters) : ICreateShortUrlUseCase

{
    public async Task<Result<CreateShortUrlOutput>> ExecuteAsync(
        CreateShortUrlInput request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        string code = await codeGenerator
            .GenerateAsync(cancellationToken);

        bool exists = await shortUrlReadRepository
            .ExistsByCodeAsync(code, cancellationToken);

        if (exists)
        {
            return Result<CreateShortUrlOutput>.FromRuleViolation(
                DomainErrors.ShortUrlErrors.ConflictAliasAlreadyInUse);
        }

        Uri sanitizedUrl = sanitizer.Sanitize(request.OriginalUrl!);

        Result<ShortUrl> shortUrl = ShortUrlFactory.Create(
            ShortCode.Create(code),
            sanitizedUrl,
            request.ExpirationDate,
            clock.UtcNow);

        await urlRepository.CreateAsync(shortUrl.Value!, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        Uri uri = ComposeShortenerUrl(code);

        CreateShortUrlOutput result =
            ComposeOutput(code, uri, shortUrl.Value!);

        return Result<CreateShortUrlOutput>.WithSuccess(result);
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

    private static CreateShortUrlOutput ComposeOutput(
        string code, Uri uri, ShortUrl shortUrl)
    {
        CreateShortUrlOutput result = new()
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
