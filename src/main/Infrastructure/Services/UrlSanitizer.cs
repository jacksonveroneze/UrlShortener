using UrlShortener.Application.Abstractions.Services;

namespace UrlShortener.Infrastructure.Services;

public class UrlSanitizer : IUrlSanitizer
{
    public Uri Sanitize(Uri url)
    {
        return url;
    }
}
