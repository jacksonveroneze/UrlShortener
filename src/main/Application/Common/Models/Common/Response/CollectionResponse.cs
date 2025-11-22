namespace UrlShortener.Application.Common.Models.Common.Response;

public abstract record CollectionResponse<TType>
    : DataResponse<ICollection<TType>>;
