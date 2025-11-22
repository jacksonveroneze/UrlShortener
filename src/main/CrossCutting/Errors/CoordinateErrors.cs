using JacksonVeroneze.NET.Result;

namespace CrossCutting.Errors;

public static partial class CommonDomainErrors
{
    public static partial class CoordinateErrors
    {
        public static Error InvalidCoordinate =>
            Error.Create("Coordinate.InvalidCoordinate",
                string.Format(TemplateDataInvalid, "coordinate"));
    }
}
