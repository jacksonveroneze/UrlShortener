using UrlShortener.Application.Abstractions.Repositories;
using ZiggyCreatures.Caching.Fusion;

namespace UrlShortener.Infrastructure.Repositories.ShortUrl;

public class ShortUrlReadCacheRepository(
    IFusionCacheProvider fusionCacheProvider,
    IShortUrlReadRepository readRepository) : IShortUrlReadCacheRepository
{
    public Task<Domain.Aggregates.Url.ShortUrl?> GetByCodeAsync(
        string code, CancellationToken cancellationToken)
    {
        IFusionCache cacheInstance = fusionCacheProvider.GetCache("ShortUrl");

        return cacheInstance.GetOrSetAsync(
            code,
            ct =>
            {
                Task<Domain.Aggregates.Url.ShortUrl?> external = readRepository
                    .GetByCodeAsync(code, ct);

                return external;
            },
            options =>
            {
                options
                    .SetDuration(duration: TimeSpan.FromMilliseconds(200))
                    .SetJittering(TimeSpan.FromSeconds(1))
                    .SetEagerRefresh(0.5f)
                    .SetFailSafe(
                        isEnabled: true,
                        maxDuration: TimeSpan.FromMinutes(30),
                        throttleDuration: TimeSpan.FromSeconds(30)
                    )
                    .SetFactoryTimeouts(
                        softTimeout: TimeSpan.FromMilliseconds(200),
                        hardTimeout: TimeSpan.FromMilliseconds(600)
                    )
                    .SetDistributedCacheTimeouts(
                        softTimeout: TimeSpan.FromMilliseconds(200),
                        hardTimeout: TimeSpan.FromMilliseconds(600)
                    );
            },
            token: cancellationToken).AsTask();
    }
}
