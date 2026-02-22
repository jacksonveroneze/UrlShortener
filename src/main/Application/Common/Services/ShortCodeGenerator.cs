using UrlShortener.Application.Abstractions.Services;

namespace UrlShortener.Application.Common.Services;

public class ShortCodeService(
    IDistributedCounter counter,
    IShortCodeGenerator generator) : IShortCodeService
{
    public async Task<string> GenerateAsync(
        CancellationToken cancellationToken)
    {
        long count = await counter
            .NextAsync(cancellationToken);

        string code = generator.Generate(count);

        return code;
    }
}
