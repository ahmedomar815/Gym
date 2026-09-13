namespace Gym.BusinessLogic.Results;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException("A successful result cannot contain an error.");
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException("A failed result must contain an error.");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public bool IsValidationFailure => IsFailure && Error.Type == ErrorType.Validation;
    public bool IsConflict => IsFailure && Error.Type == ErrorType.Conflict;
    public bool IsNotFound => IsFailure && Error.Type == ErrorType.NotFound;

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}
