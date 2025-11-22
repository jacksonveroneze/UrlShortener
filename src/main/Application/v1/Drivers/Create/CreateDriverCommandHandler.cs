using FluentValidation;
using JacksonVeroneze.NET.Result;
using MediatR;
using UrlShortener.Application.Common;
using UrlShortener.Application.Common.Extensions;
using UrlShortener.Application.Common.Interfaces;
using UrlShortener.Application.Common.Interfaces.Repositories;
using UrlShortener.Domain;
using UrlShortener.Domain.Core.Errors;
using UrlShortener.Domain.Repositories;

namespace UrlShortener.Application.v1.Drivers.Create;

public sealed class CreateDriverCommandHandler(
    ILogger<CreateDriverCommandHandler> logger,
    IEnumerable<IValidator<CreateDriverCommand>> validators,
    IDataMapper mapper,
    IDriverReadRepository driverReadRepository,
    IDriverRepository driverRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateDriverCommand, Result<CreateDriverCommandResponse>>
{
    public async Task<Result<CreateDriverCommandResponse>> Handle(
        CreateDriverCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        (bool hasErrors, Error[] errors) = FluentValidationHelper
            .ValidateRequest(request, validators);

        if (hasErrors)
        {
            return Result<CreateDriverCommandResponse>
                .FromInvalid(errors);
        }

        Result resultVerifyEmailOrDocument =
            await ExistByEmailOrDocumentAsync(
                request.Email!, cancellationToken);

        if (resultVerifyEmailOrDocument.IsFailure)
        {
            return Result<CreateDriverCommandResponse>
                .WithError(resultVerifyEmailOrDocument.FirstError!);
        }

        Result<Domain.Aggregates.Driver> resultEntity =
            FactoryEntity(request);

        if (resultEntity.IsFailure)
        {
            return Result<CreateDriverCommandResponse>
                .FromInvalid(resultEntity.Errors);
        }

        await CreateAsync(resultEntity.Value!,
            cancellationToken);

        CreateDriverCommandResponse response =
            mapper.Map<CreateDriverCommandResponse>(resultEntity.Value!);

        return Result<CreateDriverCommandResponse>.WithSuccess(response);
    }

    private async Task<Result> ExistByEmailOrDocumentAsync(
        string email,
        CancellationToken cancellationToken)
    {
        bool existsEmail = await driverReadRepository
            .ExistsByEmailAsync(email, cancellationToken);

        if (!existsEmail)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.DriverErrors.DuplicateEmailOrDocument;

        logger.LogAlreadyExists(
            ModuleConstants.ContextName,
            nameof(CreateDriverCommandHandler),
            nameof(ExistByEmailOrDocumentAsync), email, error);

        return Result.FromRuleViolation(error);
    }

    private Result<Domain.Aggregates.Driver> FactoryEntity(
        CreateDriverCommand request)
    {
        Result<Domain.Aggregates.Driver> entity = Domain.Aggregates.Driver.Create(
            request.FullName,
            request.Email!,
            request.Document);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            ModuleConstants.ContextName,
            nameof(CreateDriverCommandHandler),
            nameof(FactoryEntity), entity.Errors.Count);

        return entity;
    }

    private async Task CreateAsync(
        Domain.Aggregates.Driver driver,
        CancellationToken cancellationToken)
    {
        await driverRepository.CreateAsync(
            driver, cancellationToken);

        await unitOfWork.CommitAsync(
            cancellationToken);

        logger.LogCreated(
            ModuleConstants.ContextName,
            nameof(CreateDriverCommandHandler),
            nameof(CreateAsync), driver.Id);
    }
}
