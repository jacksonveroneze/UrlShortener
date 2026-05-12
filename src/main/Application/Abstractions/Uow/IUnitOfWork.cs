namespace UrlShortener.Application.Abstractions.Uow;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(
        CancellationToken cancellationToken);
}
