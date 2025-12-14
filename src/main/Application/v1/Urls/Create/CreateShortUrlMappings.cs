using Mapster;
using UrlShortener.Domain.Aggregates.Url;

namespace UrlShortener.Application.v1.Urls.Create;

public class CreateShortUrlMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
        
        config.NewConfig<ShortUrl, ShortUrlOutput>()
            .Map(dest => dest.ShortenedUrl, src => src.Code)
            .Map(dest => dest.CreationDate, src => src.CreatedAt)
            .Map(dest => dest.ExpirationDate, src => src.ExpiresAt);
        
        config.NewConfig<ShortUrl, CreateShortUrlOutput>()
            .Map(dest => dest.Data, src => src);
    }
}
