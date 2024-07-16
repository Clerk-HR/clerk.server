namespace clerk.server.Helpers;

public struct Result
{
    public Result(bool isSuccess, object? data = default, Error? error = null)
    {
        if (isSuccess && error != null || !isSuccess && error == null)
        {
            throw new ArgumentException("invalid response");
        };

        IsSuccess = isSuccess;
        Data = data;
        Error = error;
    }

    public bool IsSuccess { get; set; }
    public object? Data { get; set; }
    public Error? Error { get; set; }

    public static Result Success<T>(string message, T? data) => new(true, data);
    public static Result Success() => new(true);
    public static Result Failure(List<string> errors) => new(false, error: new() { Code = 400, Messages = [.. errors] });
    public static Result Exception(string exception) => new(false, error: new() { Code = 500, Messages = [exception] });
}

public struct Error
{
    public required int Code { get; set; }
    public required string[] Messages { get; set; }
}

