using JacksonVeroneze.NET.Result;
using UrlShortener.Application.Abstractions.UseCases;

namespace UrlShortener.Application.v1.Urls.Create;

public interface ICreateShortUrlUseCase :
    IUseCase<CreateShortUrlInput, Result<CreateShortUrlOutput>>;
