using UrlShortener.Application.Common.Models.Common.Response;
using UrlShortener.Application.v1.Urls.Common.Models;

namespace UrlShortener.Application.v1.Urls.GetById;

public sealed record GetByIdShortUrlOutput
    : DataResponse<ShortUrlOutput>;
