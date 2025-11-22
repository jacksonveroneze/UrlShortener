using FluentValidation;

namespace UrlShortener.Application.v1.Drivers.Create;

public class CreateDriverCommandValidator
    : AbstractValidator<CreateDriverCommand>
{
    private const int MinLengthName = 2;
    private const int MaxLengthName = 100;

    private const int LengthCpf = 11;

    public CreateDriverCommandValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.FullName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(MinLengthName, MaxLengthName);

        RuleFor(request => request.Document)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Length(LengthCpf)
            .Must(value => value!.All(char.IsNumber));

        RuleFor(request => request.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }
}
