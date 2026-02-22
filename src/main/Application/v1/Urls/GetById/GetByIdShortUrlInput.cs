using UrlShortener.Application.Abstractions.UseCases;

namespace UrlShortener.Application.v1.Urls.GetById;

public sealed record GetByIdShortUrlInput(string? Code)
    : IBaseRequest;
