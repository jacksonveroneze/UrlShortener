using CrossCutting.Helpers;
using CrossCutting.ValueObjects;
using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.NET.Result;
using UrlShortener.Domain.Core.Errors;
using UrlShortener.Domain.Enums;

namespace UrlShortener.Domain.Aggregates;

public class Driver : AggregateRoot
{
    public Guid Id { get; private set; }

    public NameValueObject Name { get; private set; } = null!;

    public EmailValueObject Email { get; private set; } = null!;

    public CpfValueObject Cpf { get; private set; } = null!;

    public DriverStatus Status { get; private set; }

    #region ctor

    protected Driver()
    {
    }

    private Driver(NameValueObject name,
        EmailValueObject email,
        CpfValueObject cpf)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(cpf);

        Id = GuidGeneratorHelper.Generate();
        Name = name;
        Email = email;
        Cpf = cpf;
        Status = DriverStatus.PendingActivation;
    }

    #endregion

    #region Factory

    public static Result<Driver> Create(
        string? name, string email, string? cpf)
    {
        Result<NameValueObject> nameVo = NameValueObject
            .Create(name);

        Result<EmailValueObject> emailVo = EmailValueObject
            .Create(email.ToLowerInvariant());

        Result<CpfValueObject> cpfVo = CpfValueObject.Create(cpf);

        Result resultValidate = Result.FailuresOrSuccess(
            nameVo, cpfVo, emailVo);

        if (resultValidate.IsFailure)
        {
            return Result<Driver>
                .FromInvalid(resultValidate.Errors);
        }

        Driver entity = new(nameVo.Value!, emailVo.Value!, cpfVo.Value!);

        return Result<Driver>.WithSuccess(entity);
    }

    #endregion
}
