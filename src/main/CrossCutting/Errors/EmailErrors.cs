using JacksonVeroneze.NET.Result;

namespace CrossCutting.Errors;

public static partial class CommonDomainErrors
{
    internal static partial class EmailError
    {
        public static Error InvalidEmail =>
            Error.Create("Email.InvalidEmail",
                string.Format(TemplateDataInvalid, "e-mail"));
    }
}
