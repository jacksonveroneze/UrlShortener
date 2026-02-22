namespace UrlShortener.Application.Abstractions.Services;

public interface IDistributedCounter
{
    Task<long> NextAsync(
        CancellationToken cancellationToken);
}
