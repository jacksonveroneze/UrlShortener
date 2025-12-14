using UrlShortener.Domain;

namespace UrlShortener.Application.Abstractions.Services;

public interface IShortCodeGenerator
{
    Task<ShortCode> GenerateAsync(
        CancellationToken cancellationToken);
}
