namespace UrlShortener.Application.Common.Models.Common.Response;

public record CoordinateResponse
{
    public float Latitude { get; init; }

    public float Longitude { get; init; }
}
