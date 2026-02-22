using JacksonVeroneze.NET.Result;

namespace UrlShortener.Domain.Aggregates.Url;

public static class ShortUrlFactory
{
    public static Result<ShortUrl> Create(
        ShortCode? code,
        Uri originalUrl,
        DateTimeOffset? expiresdAt,
        DateTimeOffset nowUtc)
    {
        ArgumentNullException.ThrowIfNull(originalUrl);
        // ArgumentNullException.ThrowIfNull(policy);

        // bool codeValid = policy.IsValidCode(code);
        // bool isValidExpiration = policy
        //     .IsValidExpiration(nowUtc, expiresdAt);
        //
        // if (!codeValid)
        // {
        //     return Result<ShortUrl>.FromRuleViolation(
        //         DomainErrors.ShortUrlErrors.InvalidCode);
        // }
        //
        // if (!isValidExpiration)
        // {
        //     return Result<ShortUrl>.FromRuleViolation(
        //         DomainErrors.ShortUrlErrors.ExpirationOutOfRange);
        // }

        return ShortUrl.Create(code, originalUrl, expiresdAt);
    }
}
