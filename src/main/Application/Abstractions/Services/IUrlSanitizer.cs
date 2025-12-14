namespace UrlShortener.Application.Abstractions.Services;

public interface IUrlSanitizer
{
    Uri Sanitize(Uri url);
}
