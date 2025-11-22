namespace UrlShortener.Application.Common.Models.Common.Response;

public abstract record DataResponse<TType> : IResponse
{
    public TType? Data { get; init; }
}
