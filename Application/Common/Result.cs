using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public IEnumerable<string> Errors { get; }

        protected Result(bool isSuccess, IEnumerable<string>? errors = null)
        {
            IsSuccess = isSuccess;
            Errors = errors ?? Enumerable.Empty<string>();
        }

        public static Result Success() => new Result(true);

        public static Result Failure(IEnumerable<IdentityError> identityErrors) 
            => new Result(false, identityErrors.Select(e => e.Description));

        public static Result Failure(string error) => new Result(false, new[] { error });
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        protected Result(bool isSuccess, T? value, IEnumerable<string>? errors = null)
            : base(isSuccess, errors)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value);

        public static Result<T> Failure(string error) => new Result<T>(false, default, new[] { error });

        public static Result<T> Failure(IEnumerable<string> errors) => new Result<T>(false, default, errors);

        public static Result<T> Failure(IEnumerable<IdentityError> identityErrors)
            => new Result<T>(false, default, identityErrors.Select(e => e.Description));
    }
}