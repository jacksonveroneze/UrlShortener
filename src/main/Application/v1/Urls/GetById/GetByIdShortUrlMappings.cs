using Mapster;
using UrlShortener.Domain.Aggregates.Url;

namespace UrlShortener.Application.v1.Urls.GetById;

public class GetByIdShortUrlMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        config.NewConfig<ShortUrl, GetByIdShortUrlOutput>()
            .Map(dest => dest.Data, src => src);
    }
}
