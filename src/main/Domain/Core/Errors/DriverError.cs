using CrossCutting.Errors;
using JacksonVeroneze.NET.Result;

namespace UrlShortener.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class ShortUrlErrors
    {
        public static Error NotFound =>
            Error.Create("ShortUrl.NotFound",
                string.Format(CommonDomainErrors.TemplateNotFound, "url"));
    }
}
