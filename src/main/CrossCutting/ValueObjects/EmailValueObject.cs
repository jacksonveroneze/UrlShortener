using System.Text.RegularExpressions;
using CrossCutting.Errors;
using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;

namespace CrossCutting.ValueObjects;

public sealed partial class EmailValueObject : ValueObject
{
    private const int MaxLength = 256;

    private const string EmailRegexPattern =
        @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    private static readonly Lazy<Regex> EmailFormatRegex =
        new(MyRegex);

    public string? Value { get; }

    internal EmailValueObject()
    {
    }

    public EmailValueObject(string value)
    {
        Value = value;
    }

    public static implicit operator string?(EmailValueObject? value)
        => value?.ToString();

    private static bool IsValid(string? value)
    {
        return !string.IsNullOrEmpty(value) &&
               value.Length <= MaxLength &&
               EmailFormatRegex.Value.IsMatch(value);
    }

    public override bool Equals(object? obj)
    {
        return Value!.Equals(obj);
    }

    internal bool Equals(EmailValueObject other)
    {
        return string.Equals(Value, other.Value,
            StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return Value != null
            ? StringComparer.OrdinalIgnoreCase.GetHashCode(Value)
            : 0;
    }

    public override string ToString()
    {
        return Value ?? string.Empty;
    }

    public static Result<EmailValueObject> Create(string? value)
    {
        if (!IsValid(value))
        {
            return Result<EmailValueObject>.FromInvalid(
                CommonDomainErrors.EmailError.InvalidEmail);
        }

        EmailValueObject valueObject = new(value!);

        return Result<EmailValueObject>.WithSuccess(valueObject);
    }

    [GeneratedRegex(
        pattern: EmailRegexPattern,
        options: RegexOptions.Compiled |
                 RegexOptions.IgnoreCase |
                 RegexOptions.ExplicitCapture,
        matchTimeoutMilliseconds: 500)]
    private static partial Regex MyRegex();
}
