using Mapster;
using UrlShortener.Application.v1.Urls.Common.Models;
using UrlShortener.Domain.Aggregates.Url;

namespace UrlShortener.Application.v1.Urls.Common.Mappings;

public class ShortUrlMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<ShortUrl, ShortUrlOutput>()
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.CreationDate, src => src.CreatedAt)
            .Map(dest => dest.ExpirationDate, src => src.ExpiresAt)
            .Ignore(dest => dest.ShortenedUrl);
    }
}
