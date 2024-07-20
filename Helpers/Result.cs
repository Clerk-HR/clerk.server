namespace clerk.server.Helpers;

public struct Result
{
    public Result(bool isSuccess, SuccessResult? success = null, ErrorResult? error = null)
    {
        if (isSuccess && error != null || !isSuccess && error == null)
        {
            throw new ArgumentException("invalid response");
        };
        IsSuccess = isSuccess;
        ErrorResult = error;
        SuccessResult = success;
    }

    public bool IsSuccess { get; set; }
    public SuccessResult? SuccessResult { get; set; }
    public ErrorResult? ErrorResult { get; set; }

    public static Result Success<T>(string message, T? data)
    => new(true, new SuccessResult { Data = data, Message = message });
    public static Result Success<T>(T data) => new(true, new SuccessResult { Data = data });
    public static Result Success(string message) => new(true, new() { Message = message });
    public static Result Failure(List<string> errors) => new(false, error: new() { Code = 400, Messages = [.. errors] });
    public static Result Unauthorized() => new(false, error: new() { Code = 401, Messages = ["unauthorized"] });
    public static Result Exception(string exception) => new(false, error: new() { Code = 500, Messages = [exception] });
}

public record SuccessResult
{
    public object? Data { get; set; }
    public string Message { get; set; } = string.Empty;
}

public record ErrorResult
{
    public required int Code { get; set; }
    public required string[] Messages { get; set; }
}

