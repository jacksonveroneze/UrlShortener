using FluentValidation;
using UrlShortener.Application.Common.Queries;

namespace UrlShortener.Application.Common.Validators;

public class PagedOffsetQueryValidator : AbstractValidator<PagedOffsetQuery>
{
    public PagedOffsetQueryValidator()
    {
        RuleFor(request => request)
            .NotNull();

        RuleFor(request => request.Page)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .GreaterThan(0);

        RuleFor(request => request.PageSize)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .GreaterThan(0);
    }
}
