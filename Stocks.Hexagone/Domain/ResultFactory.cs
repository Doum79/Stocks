namespace Stocks.Hexagone.Domain
{
    public class ResultFactory
    {
        public static Result<T> Success<T>(string? message = null, T? data = null) where T : class
        {
            return new Result<T>
            {
                Code = ResultCode.OK,
                Data = data,
                Message = message
            };

        }

        public static Result<T> Success<T>(T? data = null) where T : class
        {
            return new Result<T>
            {
                Code = ResultCode.OK,
                Data = data
            };

        }

        public static Result<T> Error<T>(string? message = null) where T : class
        {
            return new Result<T>
            {
                Code = ResultCode.BadRequest,
                Message = message
            };

        }
    }
}
