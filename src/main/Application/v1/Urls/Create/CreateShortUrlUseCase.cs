using JacksonVeroneze.NET.Result;
using UrlShortener.Application.Abstractions.Repositories;
using UrlShortener.Application.Abstractions.Services;
using UrlShortener.Application.Abstractions.Uow;
using UrlShortener.Domain;
using UrlShortener.Domain.Aggregates.Url;
using UrlShortener.Domain.Core.Errors;
using UrlShortener.Domain.Repositories;

namespace UrlShortener.Application.v1.Urls.Create;

public sealed class CreateShortUrlUseCase(
    IDataMapper mapper,
    IUrlReadRepository urlReadRepository,
    IShortUrlRepository urlRepository,
    IUnitOfWork unitOfWork,
    IUrlSanitizer sanitizer,
    IShortCodeGenerator codeGenerator,
    IDateTimeProvider clock,
    ShortUrlPolicy policy) : ICreateShortUrlUseCase

{
    public async Task<Result<CreateShortUrlOutput>> ExecuteAsync(
        CreateShortUrlInput request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        ShortCode code = await codeGenerator
            .GenerateAsync(cancellationToken);

        bool exists = await urlReadRepository
            .ExistsByCodeAsync(code.Value, cancellationToken);

        if (exists)
        {
            return Result<CreateShortUrlOutput>.FromRuleViolation(
                DomainErrors.ShortUrlErrors.ConflictAliasAlreadyInUse);
        }
        
        Uri sanitizedUrl = sanitizer.Sanitize(request.OriginalUrl!);

        Result<ShortUrl> shortUrl = ShortUrlFactory.Create(
            code, sanitizedUrl, request.ExpirationDate,
            clock.UtcNow, policy);

        await urlRepository.CreateAsync(shortUrl.Value!, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        CreateShortUrlOutput output = mapper.Map<
            ShortUrl, CreateShortUrlOutput>(shortUrl.Value!);

        return Result<CreateShortUrlOutput>.WithSuccess(output);
    }
}
