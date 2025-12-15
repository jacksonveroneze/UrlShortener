using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Application.Common.Parameters;

[ExcludeFromCodeCoverage]
public sealed record UrlShortenerParameters
{
    public const string SectionName = "UrlShortener";

    public string? Scheme { get; init; }
    
    public string? BaseDomain { get; init; }

    public string? QueryStringName { get; init; }
}
