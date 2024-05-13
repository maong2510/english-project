using Newtonsoft.Json;
using Share.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Share.Exceptions
{
    public class ApiResult<T>
    {
        [System.Text.Json.Serialization.JsonConstructor]
        public ApiResult(bool isError, string message, string resultCode)
        {
            Message = message;
            IsError = isError;
            ResultCode = resultCode;
        }

        public ApiResult(T data, bool isError = false, string message = "Success", string resultCode = ApiResultCodeConstant.Success)
        {
            Result = data;
            Message = message;
            IsError = isError;
            ResultCode = resultCode;
        }

        [JsonProperty("isError")]
        public bool IsError { get; set; } = false;

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("resultCode")]
        public string ResultCode { get; set; }

        [JsonProperty("result")]
        public T Result { get; }
    }
    public class ApiResult
    {
        [System.Text.Json.Serialization.JsonConstructor]
        public ApiResult(bool isError, string message, string resultCode)
        {
            Message = message;
            IsError = isError;
            ResultCode = resultCode;
        }

        public ApiResult(object data, bool isError = false, string message = "Success", string resultCode = ApiResultCodeConstant.Success)
        {
            Result = data;
            Message = message;
            IsError = isError;
            ResultCode = resultCode;
        }
        public ApiResult(List<object> data, bool isError = false, string message = "Success", string resultCode = ApiResultCodeConstant.Success)
        {
            Result = data;
            Message = message;
            IsError = isError;
            ResultCode = resultCode;
        }

        [JsonProperty("isError")]
        public bool IsError { get; set; } = false;

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("resultCode")]
        public string ResultCode { get; set; }

        [JsonProperty("result")]
        public object Result { get; }
        

    }
}
