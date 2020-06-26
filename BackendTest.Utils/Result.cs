using System;

namespace BackendTest.Utils
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        protected Result(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }

        public static Result Success()
        {
            return new Result(true);
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(true, value);
        }

        public static Result Failure(string errorMessage)
        {
            return new Result(errorMessage);
        }

        public static Result<T> Failure<T>(string errorMessage)
        {
            return new Result<T>(errorMessage);
        }
    }

    public class Result<T> : Result
    {
        private readonly T _value;
        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException();
                return _value;
            }
        }

        internal Result(bool isSuccess, T value)
            : base(isSuccess)
        {
            _value = value;
        }

        internal Result(string errorMessage)
            : base(errorMessage)
        {
            _value = default;
        }
    }
}
