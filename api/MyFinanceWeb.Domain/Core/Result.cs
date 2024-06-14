namespace MyFinanceWeb.Domain.Core
{
    public class Result<TValue>
    {
        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public TValue Data { get; }

        public Error Error { get; }

        public static Result<TValue> Success() => new(true, Error.None);

        public static Result<TValue> Failure(Error error) => new(false, error);
    }
}
