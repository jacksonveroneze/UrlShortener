using UrlShortener.Application.Common.Models.Common.Response;

namespace UrlShortener.Application.v1.Urls.Create;

public sealed record CreateShortUrlOutput
    : DataResponse<ShortUrlOutput>;
