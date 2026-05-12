using FluentValidation;

namespace UrlShortener.Application.v1.Urls.GetById;

public class GetByIdShortUrlValidator
    : AbstractValidator<GetByIdShortUrlInput>
{
    public GetByIdShortUrlValidator()
    {
        _ = RuleFor(request => request)
            .NotNull();

        _ = RuleFor(request => request.Code)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();
    }
}
