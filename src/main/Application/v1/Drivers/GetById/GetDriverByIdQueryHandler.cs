using JacksonVeroneze.NET.Result;
using UrlShortener.Application.Common;
using UrlShortener.Application.Common.Extensions;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Application.Common.Interfaces.Repositories;
using UrlShortener.Application.v1.Drivers.Common.Models;
using UrlShortener.Domain;
using UrlShortener.Domain.Core.Errors;

namespace UrlShortener.Application.v1.Drivers.GetById;

public interface IGetDriverByIdQueryHandler :
    IUseCase<GetDriverByIdQuery, Result<GetDriverByIdQueryResponse>>;

public sealed class GetDriverByIdQueryHandler(
    ILogger<GetDriverByIdQueryHandler> logger,
    IDataMapper mapper,
    IDriverReadRepository driverReadRepository)
    : IGetDriverByIdQueryHandler
{
    public Task<Result<GetDriverByIdQueryResponse>> ExecuteAsync(
        GetDriverByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        return InternalExecuteAsync(request, cancellationToken);
    }

    private async Task<Result<GetDriverByIdQueryResponse>> InternalExecuteAsync(
        GetDriverByIdQuery request,
        CancellationToken cancellationToken)
    {
        DriverDto? dto = await driverReadRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (dto is null)
        {
            return Result<GetDriverByIdQueryResponse>
                .FromNotFound(DomainErrors.DriverErrors.NotFound);
        }

        GetDriverByIdQueryResponse response =
            mapper.Map<DriverDto, GetDriverByIdQueryResponse>(dto);

        logger.LogGetById(
            ModuleConstants.ContextName,
            nameof(GetDriverByIdQueryHandler),
            nameof(ExecuteAsync),
            request.Id);

        return Result<GetDriverByIdQueryResponse>
            .WithSuccess(response);
    }
}
