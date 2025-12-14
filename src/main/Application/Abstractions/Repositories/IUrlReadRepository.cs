namespace UrlShortener.Application.Abstractions.Repositories;

public interface IUrlReadRepository
{
    public Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken);
}
