using JacksonVeroneze.NET.Pagination.Offset;
using Mapster;
using UrlShortener.Application.Common.Models.Common.Pagination;
using UrlShortener.Application.Common.Queries;

namespace UrlShortener.Application.Common.Mappers;

public class PaginationOffsetMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<PagedOffsetQuery, PaginationParameters>()
            .MapWith(src =>
                new PaginationParameters(src.Page!.Value,
                    src.PageSize!.Value, src.OrderBy, src.Order));

        config.NewConfig<PageInfo, PageInfoResponse>()
            .Map(dest => dest.Page, src => src.Page)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.TotalPages, src => src.TotalPages)
            .Map(dest => dest.TotalElements, src => src.TotalElements)
            .Map(dest => dest.IsFirstPage, src => src.IsFirstPage)
            .Map(dest => dest.IsLastPage, src => src.IsLastPage)
            .Map(dest => dest.HasNextPage, src => src.HasNextPage)
            .Map(dest => dest.HasBackPage, src => src.HasBackPage)
            .Map(dest => dest.NextPage, src => src.NextPage)
            .Map(dest => dest.BackPage, src => src.BackPage);
    }
}
