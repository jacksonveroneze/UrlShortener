namespace UrlShortener.Application.Abstractions.Services;

public interface IDateTimeProvider
{
    public DateTimeOffset UtcNow { get; }

    public DateTime Now { get; }

    public DateOnly DateNow { get; }

    public TimeOnly TimeNow { get; }
}
