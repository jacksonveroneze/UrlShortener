namespace UrlShortener.Application.Abstractions.Services;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }

    DateTimeOffset Now { get; }

    DateOnly DateNow { get; }

    TimeOnly TimeNow { get; }
}
