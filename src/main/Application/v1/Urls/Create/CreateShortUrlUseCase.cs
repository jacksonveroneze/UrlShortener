using JacksonVeroneze.NET.Result;

namespace UrlShortener.Application.v1.Urls.Create;

public sealed class CreateShortUrlUseCase : ICreateShortUrlUseCase

{
    public Task<Result<CreateShortUrlOutput>> ExecuteAsync(
        CreateShortUrlInput request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        throw new NotSupportedException();
    }
}
