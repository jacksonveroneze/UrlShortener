using MapsterMapper;
using UrlShortener.Application.Common.Interfaces;

namespace UrlShortener.Infrastructure.Mapper;

public class AutoMapperAdapter(IMapper mapper) : IDataMapper
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
