using UrlShortener.Application.Abstractions.Services;
using UrlShortener.Domain;

namespace UrlShortener.Infrastructure.Services;

public class ShortCodeGenerator : IShortCodeGenerator
{
    public Task<ShortCode> GenerateAsync(
        CancellationToken cancellationToken)
    {
        throw new NotSupportedException();
    }
}
