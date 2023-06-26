using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Hexagone.Domain
{

    public enum ResultCode
    {
        OK = 200,
        BadRequest = 400
    }
    public class Result<T>
    {
        public ResultCode Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public bool IsSuccess() => Code == ResultCode.OK;
        public bool IsError() => Code != ResultCode.OK;

    }

}
