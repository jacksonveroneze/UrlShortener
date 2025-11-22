namespace UrlShortener.Application.Common.Interfaces;

public interface IDataMapper
{
    TDestination Map<TDestination>(object source);

    TDestination Map<TSource, TDestination>(TSource source);
}
