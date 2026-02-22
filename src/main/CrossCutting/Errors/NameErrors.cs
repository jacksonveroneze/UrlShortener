using JacksonVeroneze.NET.Result;

namespace CrossCutting.Errors;

public static partial class CommonDomainErrors
{
    internal static partial class NameError
    {
        public static Error InvalidName =>
            Error.Create("Name.InvalidName",
                string.Format(TemplateDataInvalid, "name"));
    }
}
