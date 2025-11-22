namespace UrlShortener.Domain.Enums;

public enum StatusMotorista
{
    None,
    Available = 1,
    Absent = 2,
    Loading = 3,
    Loaded = 4,
    InTransit = 5,
    TripCompleted = 6,
}
