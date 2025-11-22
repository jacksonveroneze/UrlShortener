using JacksonVeroneze.NET.Result;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Api.Endpoints.Extensions;

internal static class ResultTranslator
{
    public static IResult ToIResult(this Result result)
    {
        ArgumentNullException.ThrowIfNull(result);

        return result.IsSuccess
            ? Results.NoContent()
            : CreateProblemDetailsResult(result);
    }

    public static IResult ToIResult<T>(this Result<T> result)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (!result.IsSuccess)
        {
            return CreateProblemDetailsResult(result);
        }

        return result.Value is null
            ? Results.NoContent()
            : Results.Ok(result.Value);
    }

    private static IResult ToCreatedResult<T>(
        this Result<T> result,
        Uri? locationUri = null)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (result.IsFailure)
        {
            return CreateProblemDetailsResult(result);
        }

        return locationUri is not null
            ? Results.Created(locationUri, result.Value)
            : Results.StatusCode(StatusCodes.Status201Created);
    }

    public static IResult ToCreatedResultFromRoute<T>(
        this Result<T> result,
        LinkGenerator linkGenerator,
        HttpContext context,
        string routeName,
        object id)
    {
        ArgumentNullException.ThrowIfNull(result);
        
        if (!result.IsSuccess)
        {
            return result.ToCreatedResult();
        }

        Uri uri = LocationBuilder.ForNamedRoute(
            linkGenerator, context, routeName, id);

        return result.ToCreatedResult(uri);
    }


    private static IResult CreateProblemDetailsResult(Result result)
    {
        int statusCode = MapStatusCode(result.Type);

        ProblemDetails problem = new()
        {
            Status = statusCode,
            Title = GetTitle(result.Type),
            Detail = "One or more validation errors occurred.",
            Extensions =
            {
                ["errors"] = result.ToDictionaryByTarget,
            },
        };

        return Results.Problem(
            title: problem.Title,
            detail: problem.Detail,
            statusCode: problem.Status,
            extensions: problem.Extensions);
    }

    private static int MapStatusCode(ResultType resultType)
    {
        return resultType switch
        {
            ResultType.Success => StatusCodes.Status200OK,
            ResultType.Invalid => StatusCodes.Status400BadRequest,
            ResultType.Conflict => StatusCodes.Status409Conflict,
            ResultType.NotFound => StatusCodes.Status404NotFound,
            ResultType.RuleViolation => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError,
        };
    }

    private static string GetTitle(ResultType resultType)
    {
        return resultType switch
        {
            ResultType.Invalid => "Invalid Request",
            ResultType.Conflict => "Conflict Detected",
            ResultType.NotFound => "Resource Not Found",
            ResultType.RuleViolation => "Business Rule Violation",
            ResultType.Error => "Internal Server Error",
            _ => "Operation Failed",
        };
    }
}
