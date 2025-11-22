using CrossCutting.Errors;
using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;

namespace CrossCutting.ValueObjects;

public class CoordinateValueObject : ValueObject
{
    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public CoordinateValueObject()
    {
    }

    public CoordinateValueObject(
        double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static implicit operator string?(CoordinateValueObject? value)
        => value?.ToString();

    private static bool IsValid(double latitude, double longitude)
    {
        return latitude is >= -90 and <= 90 &&
               longitude is >= -100 and <= 100;
    }

    public override string ToString()
    {
        return $"Lat: {Latitude} - Lon: {Longitude}";
    }

    public static Result<CoordinateValueObject> Create(
        double latitude, double longitude)
    {
        if (!IsValid(latitude, longitude))
        {
            return Result<CoordinateValueObject>.FromInvalid(
                CommonDomainErrors.CoordinateErrors.InvalidCoordinate);
        }

        CoordinateValueObject valueObject = new(latitude, longitude);

        return Result<CoordinateValueObject>.WithSuccess(valueObject);
    }
}
