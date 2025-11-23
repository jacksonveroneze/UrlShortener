namespace UrlShortener.Application.Abstractions.Repositories;

public interface IUrlReadRepository
{
    public Task<bool> ExistsByUrlAsync(
        Uri url,
        CancellationToken cancellationToken);
}
