using FluentValidation;

namespace UrlShortener.Application.v1.Urls.Create;

public class CreateShortUrlValidator
    : AbstractValidator<CreateShortUrlInput>
{
    private const int MinLengthCustomAlias = 7;
    private const int MaxLengthCustomAlias = 7;

    public CreateShortUrlValidator()
    {
        _ = RuleFor(request => request)
            .NotNull();

        _ = RuleFor(request => request.OriginalUrl)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();

        _ = RuleFor(request => request.CustomAlias)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .Length(MinLengthCustomAlias, MaxLengthCustomAlias)
            .Must(value => value!.All(char.IsAsciiLetterOrDigit));

        _ = RuleFor(request => request.ExpirationDate)
            .Cascade(CascadeMode.Stop)
            .NotNull();
    }
}
