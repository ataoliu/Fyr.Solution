namespace Fyr.Shared.RESTful;
public interface IResultProvider
{
    ApiResponse<T> Success<T>(T data, string message = "Request successful", int code = 200);
    ApiResponse<T> Error<T>(string message, int code = 500);
}