namespace Fyr.RESTful;
public class ResultProvider : IResultProvider
{
    public ApiResponse<T> Success<T>(T data, string message = "Request successful", int code = 200)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            Code = code
        };
    }

    public ApiResponse<T> Error<T>(string message, int code = 500)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Data = default(T),
            Code = code
        };
    }
}
