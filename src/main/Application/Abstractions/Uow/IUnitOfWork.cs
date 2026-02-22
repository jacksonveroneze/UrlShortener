namespace UrlShortener.Application.Abstractions.Uow;

public interface IUnitOfWork
{
    public Task<bool> CommitAsync(
        CancellationToken cancellationToken);
}
