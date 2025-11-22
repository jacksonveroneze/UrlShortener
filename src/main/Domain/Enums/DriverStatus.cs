namespace UrlShortener.Domain.Enums;

public enum DriverStatus
{
    None,
    PendingActivation,
    Active,
    Inactive,
    BlockedTemporarily,
    BlockedPermanent,
}
