using System.Linq.Expressions;
using CrossCutting.Builders;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.v1.Drivers.Common.Filters;
using UrlShortener.Application.v1.Drivers.Common.Models;

namespace UrlShortener.Infrastructure.Builders.Filters;

public class DriverPagedFilterBuilder(
    DriverPagedFilter filter)
{
    public static DriverPagedFilterBuilder Create(
        DriverPagedFilter filter)
    {
        return new DriverPagedFilterBuilder(filter);
    }

    public Expression<Func<DriverDto, bool>> Build()
    {
        FilterBuilder<DriverDto> builder = new();

        if (filter.Status.HasValue)
        {
            builder.And(d => filter.Status!.Value
                             == filter.Status.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            builder.And(d => EF.Functions
                .ILike(d.FullName!, $"%{filter.Name}%"));
        }

        return builder.Build();
    }
}
