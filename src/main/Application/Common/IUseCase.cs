using JacksonVeroneze.NET.Result;

namespace UrlShortener.Application.Common;

public interface IUseCase<TRequest, TResponse> 
    where TRequest : IBaseRequest
    where TResponse : Result
{
    Task<TResponse> ExecuteAsync(
        TRequest request,
        CancellationToken cancellationToken);
}

public interface IBaseRequest;
public interface IResponse;
