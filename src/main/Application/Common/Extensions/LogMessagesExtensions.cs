using JacksonVeroneze.NET.Result;

namespace UrlShortener.Application.Common.Extensions;

public static partial class LogMessagesExtensions
{
    #region CommonError

    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Error,
        Message = "{contextName} - {className} - {methodName} - Error - Message: '{message}'")]
    public static partial void LogGenericError(this ILogger logger,
        string contextName, string className, string methodName, string message);

    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Error,
        Message = "{contextName} - {className} - {methodName} - Error - Count: '{count}'")]
    public static partial void LogGenericError(this ILogger logger,
        string contextName, string className, string methodName, int count);

    [LoggerMessage(
        EventId = 1002,
        Level = LogLevel.Error,
        Message = "{contextName} - {className} - {methodName} - Error - " +
                  "Identifier: '{identifier}' - Message: '{message}'")]
    public static partial void LogGenericError(this ILogger logger,
        string contextName, string className, string methodName, 
        Guid identifier, string message);

    #endregion

    #region CommonValidation

    [LoggerMessage(
        EventId = 1002,
        Level = LogLevel.Warning,
        Message = "{contextName} - {className} - {methodName} - Error: '{error}'")]
    public static partial void LogGenericValidationError(this ILogger logger,
        string contextName, string className, string methodName, Error error);

    [LoggerMessage(
        EventId = 1003,
        Level = LogLevel.Warning,
        Message = "{contextName} - {className} - {methodName} - " +
                  "Identifier: '{identifier}' - Error: '{error}'")]
    public static partial void LogGenericValidationError(this ILogger logger,
        string contextName, string className, string methodName, Guid identifier, Error error);

    [LoggerMessage(
        EventId = 1031,
        Level = LogLevel.Warning,
        Message = "{contextName} - {className} - {methodName} - " +
                  "Identifier: '{identifier}' - RuleViolation - Message: '{message}'")]
    public static partial void LogRuleViolation(this ILogger logger,
        string contextName, string className, string methodName, 
        Guid identifier, string message);
    
    #endregion

    #region CommonAlreadyExists

    [LoggerMessage(
        EventId = 1011,
        Level = LogLevel.Warning,
        Message = "{contextName} - {className} - {methodName} - " +
                  "Identifier: '{identifier}' - AlreadyExists - Message: '{message}'")]
    public static partial void LogAlreadyExists(this ILogger logger,
        string contextName, string className, string methodName, Guid identifier, string message);

    [LoggerMessage(
        EventId = 1012,
        Level = LogLevel.Warning,
        Message = "{contextName} - {className} - {methodName} - " +
                  "Identifier: '{identifier}' - AlreadyExists - Message: '{message}'")]
    public static partial void LogAlreadyExists(this ILogger logger,
        string contextName, string className, string methodName, string identifier, string message);

    #endregion

    #region CommonProcessed

    [LoggerMessage(
        EventId = 1030,
        Level = LogLevel.Information,
        Message = "{contextName} - {className} - {methodName} - " +
                  "Identifier: '{identifier}' - Processed Success")]
    public static partial void LogProcessed(this ILogger logger,
        string contextName, string className, string methodName, Guid identifier);

    #endregion

    #region CommonGetData

    [LoggerMessage(
        EventId = 1040,
        Level = LogLevel.Information,
        Message = "{contextName} - {className} - {methodName} - " +
                  "Id: '{id}'")]
    public static partial void LogGetById(this ILogger logger,
        string contextName, string className, string methodName,
        Guid id);
    
    [LoggerMessage(
        EventId = 1041,
        Level = LogLevel.Information,
        Message = "{contextName} - {className} - {methodName} - " +
                  "PageSize: '{pageSize}' - " +
                  "TotalElements: '{totalElements}' - " +
                  "TotalPages: '{totalPages}'")]
    public static partial void LogGetPagedWithOffset(this ILogger logger,
        string contextName, string className, string methodName,
        int pageSize, int totalElements, int totalPages);

    #endregion

    #region CommonCreated

    [LoggerMessage(
        EventId = 1050,
        Level = LogLevel.Information,
        Message = "{contextName} - {className} - {methodName} - " +
                  "Identifier: '{identifier}' - Created")]
    public static partial void LogCreated(this ILogger logger,
        string contextName, string className, string methodName, Guid identifier);

    #endregion

    #region CommonUpdated

    [LoggerMessage(
        EventId = 1060,
        Level = LogLevel.Information,
        Message = "{contextName} - {className} - {methodName} - " +
                  "Identifier: '{identifier}' - Updated")]
    public static partial void LogUpdated(this ILogger logger,
        string contextName, string className, string methodName, Guid identifier);

    #endregion

    #region CommonDeleted

    [LoggerMessage(
        EventId = 1070,
        Level = LogLevel.Information,
        Message = "{contextName} - {className} - {methodName} - " +
                  "Identifier: '{identifier}' - Deleted")]
    public static partial void LogDeleted(this ILogger logger,
        string contextName, string className, string methodName, Guid identifier);

    #endregion
}
