using FluentValidation;
using UrlShortener.Application.Common.Validators;

namespace UrlShortener.Application.v1.Drivers.GetById;

public class GetDriverByIdQueryValidator
    : AbstractValidator<GetDriverByIdQuery>
{
    public GetDriverByIdQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Id)
            .SetValidator(new IdGuidValidator());
    }
}
