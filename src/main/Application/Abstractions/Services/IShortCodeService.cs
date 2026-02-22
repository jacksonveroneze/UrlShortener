namespace UrlShortener.Application.Abstractions.Services;

public interface IShortCodeService
{
    Task<string> GenerateAsync(
        CancellationToken cancellationToken);
}
