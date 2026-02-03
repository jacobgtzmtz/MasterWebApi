namespace Application.Core;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public T? Value { get; set; }
    public string? Error { get; set; }

    public static Result<T> Success(T value)
    {
        Result<T> result = new Result<T>();
        result.IsSuccess = true;
        result.Value = value;
        return result;
    }

    public static Result<T> Failure(string error)
    {
        Result<T> result = new Result<T>();
        result.IsSuccess = false;
        result.Error = error;
        return result;
    }

}
