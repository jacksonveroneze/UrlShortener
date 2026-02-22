namespace UrlShortener.Domain.Aggregates.Url;

public sealed class ShortUrlPolicy
{
    private readonly HashSet<char> _alphabet;

    public ShortUrlPolicy(
        string allowedAlphabet,
        int codeMinLength,
        int codeMaxLength,
        int aliasMinLength,
        int aliasMaxLength,
        IEnumerable<string>? reservedAliases,
        IEnumerable<string>? blockedAliases,
        TimeSpan minExpiration,
        TimeSpan maxExpiration)
    {
        if (string.IsNullOrWhiteSpace(allowedAlphabet))
        {
            throw new ArgumentException("Allowed alphabet is required.", nameof(allowedAlphabet));
        }

        if (codeMinLength <= 0 || codeMaxLength < codeMinLength)
        {
            throw new ArgumentOutOfRangeException(nameof(codeMinLength));
        }

        if (aliasMinLength <= 0 || aliasMaxLength < aliasMinLength)
        {
            throw new ArgumentOutOfRangeException(nameof(aliasMinLength));
        }

        ArgumentOutOfRangeException.ThrowIfLessThan(minExpiration, TimeSpan.Zero);
        ArgumentOutOfRangeException.ThrowIfLessThan(maxExpiration, minExpiration);

        AllowedAlphabet = allowedAlphabet;
        CodeMinLength = codeMinLength;
        CodeMaxLength = codeMaxLength;
        AliasMinLength = aliasMinLength;
        AliasMaxLength = aliasMaxLength;
        MinExpiration = minExpiration;
        MaxExpiration = maxExpiration;

        _alphabet = new HashSet<char>(allowedAlphabet);
    }


    public string AllowedAlphabet { get; }
    public int CodeMinLength { get; }
    public int CodeMaxLength { get; }
    public int AliasMinLength { get; }
    public int AliasMaxLength { get; }
    public TimeSpan MinExpiration { get; }
    public TimeSpan MaxExpiration { get; }

    public bool IsValidCode(ShortCode? code)
    {
        return code is not null
               && LengthBetween(code.Value.Length, CodeMinLength, CodeMaxLength)
               && AllCharsAllowed(code.Value);
    }

    public bool IsValidExpiration(DateTimeOffset nowUtc, DateTimeOffset? expiresAtUtc)
    {
        if (expiresAtUtc is null)
        {
            return true; // Sem expiração é válido
        }

        if (expiresAtUtc <= nowUtc + MinExpiration)
        {
            return false;
        }

        return !(expiresAtUtc > nowUtc + MaxExpiration);
    }

    private static bool LengthBetween(int length, int min, int max)
    {
        return length >= min && length <= max;
    }

    private bool AllCharsAllowed(string value)
    {
        return value.All(t => _alphabet.Contains(t));
    }
}
