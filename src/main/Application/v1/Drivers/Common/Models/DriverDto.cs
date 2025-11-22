using UrlShortener.Domain.Enums;

namespace UrlShortener.Application.v1.Drivers.Common.Models;

public sealed record DriverDto
{
    public Guid? Id { get; init; }

    public string? FullName { get; init; }

    public string? Document { get; init; }

    public string? Email { get; init; }

    public DriverStatus? Status { get; init; }

    public DateTime? DeletedAt { get; init; }
}
