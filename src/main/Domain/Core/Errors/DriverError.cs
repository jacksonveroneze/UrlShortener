using CrossCutting.Errors;
using JacksonVeroneze.NET.Result;

namespace UrlShortener.Domain.Core.Errors;

public static partial class DomainErrors
{
    public static class DriverErrors
    {
        public static Error NotFound =>
            Error.Create("Driver.NotFound",
                string.Format(CommonDomainErrors.TemplateNotFound, "driver"));

        public static Error DuplicateEmailOrDocument =>
            Error.Create("Driver.DuplicateEmailOrDocument",
                string.Format(CommonDomainErrors.TemplateDataInUse, 
                    "e-mail or document"));

        public static Error InvalidTransitionStatus =>
            Error.Create("Driver.InvalidTransitionStatus", 
                "Invalid Transition Status.");
        
        public static Error NotPendingApproval =>
            Error.Create("Driver.NotPendingApproval", 
                "Only pending Drivers can be approved.");

        public static Error AlreadyBlockPermanently =>
            Error.Create("Driver.AlreadyBlockPermanently", 
                "The Driver is already permanently blocked.");

        public static Error AlreadyBlockedTemporarily =>
            Error.Create("Driver.AlreadyBlockedTemporarily", 
                "The Driver is already temporarily blocked.");

        public static Error AlreadyInactivated =>
            Error.Create("Driver.AlreadyInactivated", 
                "The Driver has already been inactivated.");

        public static Error AlreadyActive =>
            Error.Create("Driver.AlreadyActive", 
                "The Driver is already active.");

        public static Error NotActivated =>
            Error.Create("Driver.NotActivated", 
                "The Driver is not activated.");

        public static Error HasActiveRide =>
            Error.Create("Driver.HasActiveRide",
                "The driver cannot perform this action because he has an active race.");
    }
}
