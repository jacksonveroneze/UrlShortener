using HashidsNet;
using UrlShortener.Application.Abstractions.Services;
using UrlShortener.Infrastructure.Configurations;

namespace UrlShortener.Infrastructure.Services;

public class ShortCodeGeneratorHashIds(
    ShortCodeHashIdsOptions options) : IShortCodeGenerator
{
    public string Generate(long value)
    {
        Hashids hashids = new(
            salt: options.Salt,
            minHashLength: options.MinLength,
            alphabet: options.Alphabet);

        string? hash = hashids.EncodeLong(value);

        return hash;
    }
}
