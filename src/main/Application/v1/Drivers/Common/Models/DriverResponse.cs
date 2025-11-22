using UrlShortener.Domain.Enums;

namespace UrlShortener.Application.v1.Drivers.Common.Models;

public sealed record DriverResponse
{
    public Guid Id { get; init; }

    public required string FullName { get; init; }

    public required string Document { get; init; }

    public required DriverStatus Status { get; init; }
}
