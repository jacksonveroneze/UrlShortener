namespace UrlShortener.Application.Abstractions.Repositories;

public interface IShortUrlReadRepository
{
    public Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken);
}
