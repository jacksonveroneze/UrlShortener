namespace UrlShortener.Domain;

public sealed class ShortCode : IEquatable<ShortCode>
{
// EF Core
    private ShortCode() { Value = string.Empty; }


    private ShortCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Short code cannot be null or whitespace.", nameof(value));
        if (HasWhitespace(value))
            throw new ArgumentException("Short code cannot contain whitespace.", nameof(value));


        Value = value; // manter case-sensitivity por decisão de ADR
    }


    public string Value { get; private set; }
    public int ValueLength => Value.Length;


    public static ShortCode Create(string value) => new ShortCode(value);


    public static bool TryCreate(string? value, out ShortCode? result, out string? error)
    {
        result = null; error = null;
        if (string.IsNullOrWhiteSpace(value)) { error = "Short code cannot be null or whitespace."; return false; }
        if (HasWhitespace(value)) { error = "Short code cannot contain whitespace."; return false; }
        result = new ShortCode(value);
        return true;
    }


    public override string ToString() => Value;


    public bool Equals(ShortCode? other)
        => other is not null && ReferenceEquals(this, other) || (other is not null && Value == other.Value);


    public override bool Equals(object? obj) => obj is ShortCode sc && Equals(sc);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);


    public static bool operator ==(ShortCode? left, ShortCode? right) => Equals(left, right);
    public static bool operator !=(ShortCode? left, ShortCode? right) => !Equals(left, right);


    private static bool HasWhitespace(string s)
    {
        for (int i = 0; i < s.Length; i++)
            if (char.IsWhiteSpace(s[i])) return true;
        return false;
    }
}
