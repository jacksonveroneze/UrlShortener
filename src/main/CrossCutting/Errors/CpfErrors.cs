using JacksonVeroneze.NET.Result;

namespace CrossCutting.Errors;

public static partial class CommonDomainErrors
{
    internal static partial class CpfErrors
    {
        public static Error InvalidCpf =>
            Error.Create("Cpf.InvalidCpf",
                string.Format(TemplateDataInvalid, "cpf"));
    }
}
