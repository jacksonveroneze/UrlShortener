using JacksonVeroneze.NET.Result;

namespace UrlShortener.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class ShortUrlErrors
    {
        public static Error InvalidCode =>
            Error.Create("ShortUrl.InvalidCode",
                "The provided short code is invalid or does not satisfy the policy.");

        public static Error ExpirationOutOfRange =>
            Error.Create("ShortUrl.ExpirationOutOfRange",
                "The expiration timestamp is outside the allowed window.");
        
        public static Error ConflictAliasAlreadyInUse =>
            Error.Create("ShortUrl.Conflict",
                $"The alias is already in use.");
        
        public static Error NotFound =>
            Error.Create("ShortUrl.NotFound",
                $"The shortUrl not found");
    }
}
