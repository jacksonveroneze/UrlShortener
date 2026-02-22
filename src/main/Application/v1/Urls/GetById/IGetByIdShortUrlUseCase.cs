using UrlShortener.Application.Abstractions.UseCases;

namespace UrlShortener.Application.v1.Urls.GetById;

public interface IGetByIdShortUrlUseCase :
    IUseCase<GetByIdShortUrlInput, GetByIdShortUrlOutput>;
