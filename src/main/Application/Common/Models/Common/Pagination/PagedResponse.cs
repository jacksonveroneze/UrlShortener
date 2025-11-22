namespace UrlShortener.Application.Common.Models.Common.Pagination;

public record PagedResponse<TType>
{
    public ICollection<TType>? Data { get; init; }

    public PageInfoResponse? Pagination { get; init; }
}
