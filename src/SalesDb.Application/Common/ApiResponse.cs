namespace SalesDb.Application.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public static ApiResponse<T> Ok(T data, string? msg = null) => new() { Success = true, StatusCode = 200, Data = data, Message = msg };
    public static ApiResponse<T> Fail(string msg, int code = 400) => new() { Success = false, StatusCode = code, Message = msg };
}