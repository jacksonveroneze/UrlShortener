using UrlShortener.Application.Common.Models.Common.Response;
using UrlShortener.Application.v1.Drivers.Common.Models;

namespace UrlShortener.Application.v1.Drivers.GetById;

public sealed record GetDriverByIdQueryResponse
    : DataResponse<DriverResponse>;
