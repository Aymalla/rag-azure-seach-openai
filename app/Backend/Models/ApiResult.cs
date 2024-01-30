using System.Net;

namespace backend.Models;

public class ApiResult
{
    public object? Result { get; set; }
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the status code of the API result.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    public static ApiResult OK(object? result)
    {
        return new ApiResult
        {
            Result = result,
            Success = true,
            StatusCode = HttpStatusCode.OK
        };
    }

    public static ApiResult BadRequest(string message)
    {
        return new ApiResult
        {
            Result = message,
            Success = false,
            StatusCode = HttpStatusCode.BadRequest
        };
    }

    public static ApiResult NotFound(string message)
    {
        return new ApiResult
        {
            Result = message,
            Success = false,
            StatusCode = HttpStatusCode.NotFound
        };
    }

    public static ApiResult InternalServerError(string message)
    {
        return new ApiResult
        {
            Result = message,
            Success = false,
            StatusCode = HttpStatusCode.InternalServerError
        };
    }
}