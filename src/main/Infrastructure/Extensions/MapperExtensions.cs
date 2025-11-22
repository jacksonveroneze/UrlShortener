using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application;

namespace UrlShortener.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MapperExtensions
{
    public static IServiceCollection AddMapper(
        this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        // Escaneia o assembly da Application para encontrar classes que implementam IRegister
        // Isso vai localizar e executar o Register do seu PaginationOffsetMapper
        config.Scan(typeof(AssemblyReference).Assembly);

        // Registra a configuração como Singleton para ser usada globalmente
        services.AddSingleton(config);

        // Registra o IMapper (ServiceMapper) para injeção de dependência
        // Isso permite usar: public MyClass(IMapper mapper) { ... }
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
