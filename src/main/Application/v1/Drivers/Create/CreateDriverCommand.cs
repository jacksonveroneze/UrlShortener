using JacksonVeroneze.NET.Result;
using MediatR;

namespace UrlShortener.Application.v1.Drivers.Create;

public sealed record CreateDriverCommand
    : IRequest<Result<CreateDriverCommandResponse>>
{
    public string? FullName { get; init; }

    public string? Document { get; init; }

    public string? Email { get; init; }
}
