namespace UrlShortener.Application.v1.Drivers.GetById;

public sealed record GetDriverByIdQuery(Guid Id) : UrlShortener.Application.Common.IBaseRequest;
