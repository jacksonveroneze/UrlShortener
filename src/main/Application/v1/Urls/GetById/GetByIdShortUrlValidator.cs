using FluentValidation;

namespace UrlShortener.Application.v1.Urls.GetById;

public class GetByIdShortUrlValidator
    : AbstractValidator<GetByIdShortUrlInput>
{
    public GetByIdShortUrlValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Code)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();
    }
}
