using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Pagination.Offset;

namespace UrlShortener.Application.v1.Drivers.Common.Filters;

[ExcludeFromCodeCoverage]
public record DriverPagedFilter : DriverFilter
{
    public string? Name { get; init; }

    public PaginationParameters? Pagination { get; init; }
}
