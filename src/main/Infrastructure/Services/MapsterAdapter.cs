using MapsterMapper;
using UrlShortener.Application.Abstractions.Services;

namespace UrlShortener.Infrastructure.Services;

public class MapsterAdapter(IMapper mapper) : IDataMapper
{
    public TDestination Map<TDestination>(object source)
    {
        return mapper.Map<TDestination>(source!);
    }

    public TDestination Map<TSource, TDestination>(
        TSource source)
    {
        return mapper.Map<TSource, TDestination>(source!);
    }
}
