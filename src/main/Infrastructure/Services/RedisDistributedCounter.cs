using StackExchange.Redis;
using UrlShortener.Application.Abstractions.Services;

namespace UrlShortener.Infrastructure.Services;

public class RedisDistributedCounter(
    IConnectionMultiplexer connectionMultiplexer) : IDistributedCounter
{
    private const int DefaultDatabase = 0;
    private const string DefaultPrefixCounter = "distributedCounter";

    public Task<long> NextAsync(
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IDatabase database = connectionMultiplexer
            .GetDatabase(db: DefaultDatabase);

        RedisKey key = new(key: DefaultPrefixCounter);

        Task<long> counter = database
            .StringIncrementAsync(key: key);

        return counter;
    }
}
