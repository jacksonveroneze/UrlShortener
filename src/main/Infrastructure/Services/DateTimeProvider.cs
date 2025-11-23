using UrlShortener.Application.Abstractions.Services;

namespace UrlShortener.Infrastructure.Services;

[ExcludeFromCodeCoverage]
internal sealed class DateTimeProvider : IDateTimeProvider
{
    private const string WindowsBrTimeZone
        = "E. South America Standard Time";

    private const string LinuxBrTimeZone
        = "America/Sao_Paulo";

    private static readonly TimeZoneInfo TzInfo
        = GetTimeZoneInfo();

    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Now => TimeZoneInfo
        .ConvertTimeFromUtc(UtcNow, TzInfo);

    public DateOnly DateNow =>
        DateOnly.FromDateTime(Now);

    public TimeOnly TimeNow =>
        TimeOnly.FromDateTime(Now);

    private static TimeZoneInfo GetTimeZoneInfo()
    {
        return TimeZoneInfo
            .FindSystemTimeZoneById(GetTimeZoneId());
    }

    private static string GetTimeZoneId()
    {
        if (OperatingSystem.IsWindows())
        {
            return WindowsBrTimeZone;
        }

        return OperatingSystem.IsLinux()
               || OperatingSystem.IsMacOS()
            ? LinuxBrTimeZone
            : throw new InvalidOperationException("Invalid plataform");
    }
}
