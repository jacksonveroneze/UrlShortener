using UrlShortener.Application.Common.Models.Common.Response;
using UrlShortener.Application.v1.Drivers.Common.Models;

namespace UrlShortener.Application.v1.Drivers.Create;

public sealed record CreateDriverCommandResponse
    : DataResponse<DriverResponse>;
