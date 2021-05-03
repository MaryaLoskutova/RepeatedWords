using System.Diagnostics.CodeAnalysis;

namespace TextAnalyzer.Service.BusinessObjects
{
    public class Result<T>
    {
        public T Value { get; }
        public string Message { get; }
        public bool IsSuccess => string.IsNullOrEmpty(Message);
        protected Result(T value)
        {
            Value = value;
        }
        protected Result(string message)
        {
            Message = message;
        }

        public static Result<T> Fail(string message)
        {
            return new(message);
        }

        public static Result<T> Ok(T value)
        {
            return new(value);
        }
    }
}