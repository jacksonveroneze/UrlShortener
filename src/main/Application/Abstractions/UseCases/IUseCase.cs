using JacksonVeroneze.NET.Result;

namespace UrlShortener.Application.Abstractions.UseCases;

public interface IBaseRequest;

public interface IResponse;

public interface IUseCase<in TRequest, TResponse>
    where TRequest : IBaseRequest
{
    Task<Result<TResponse>> ExecuteAsync(
        TRequest request,
        CancellationToken cancellationToken);
}
