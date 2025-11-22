using System.Diagnostics.CodeAnalysis;
using UrlShortener.Domain.Enums;

namespace UrlShortener.Application.v1.Drivers.Common.Filters;

[ExcludeFromCodeCoverage]
public record DriverFilter
{
    public DriverStatus? Status { get; init; }
}
