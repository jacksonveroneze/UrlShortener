namespace UrlShortener.Application.Common.Interfaces;

public interface IUnitOfWork
{
    public Task<bool> CommitAsync(
        CancellationToken cancellationToken);
}
