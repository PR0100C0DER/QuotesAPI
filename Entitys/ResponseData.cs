using Newtonsoft.Json;
using QuotesAPI.Enums;
using QuotesAPI.Helper;
using System;

namespace QuotesAPI.Entitys
{
    public class ResponseData
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int StatusCode { get; set; }
        public object Result { get; set; }
        public object Error { get; set; }
        public string Id { get; set; }

        public ResponseData(ResponseData response)
        {
            StatusCode = response.StatusCode;
            Result = response.Result;
            Error = response.Error;
        }

        public ResponseData(object obj, ResponseStatusCode code)
        {
            StatusCode = HttpContextHelper.GetStatusCode(code);

            switch (StatusCode)
            {
                case 200:
                case 201:
                case 202:
                    Result = obj;
                    break;
                default:
                    Error = obj;
                    break;
            }
        }

        public ResponseData(string error)
        {
            HttpContextHelper.SetStatusCode(ResponseStatusCode.BadRequest);
            StatusCode = 400;
            Result = null;
            Error = new { code = 400, message = error };
        }

        public ResponseData()
        {
            Result = null;
            Error = null;
        }

        public ResponseData(bool isSuccess)
        {
            StatusCode = HttpContextHelper.GetStatusCode(ResponseStatusCode.OK);
            Result = new { success = isSuccess }; ;
        }

        public ResponseData(ModelStateDictionary modelState)
        {
            StatusCode = HttpContextHelper.GetStatusCode(ResponseStatusCode.BadRequest);

            Error = modelState.Keys
                    .SelectMany(key => modelState[key]
                                            .Errors
                                            .Select(x => new ValidationError { Message = $"\"{key} -> {x.ErrorMessage}\" не соответствует" }))
                                            .ToList();
        }

        public ResponseData(Exception ex)
        {
            StatusCode = HttpContextHelper.GetStatusCode(ResponseStatusCode.BadRequest);
            Error = new
            {
                code = (int)ResponseStatusCode.BadRequest,
                message = ex.Message + " " + ex.InnerException?.Message
            };
        }

        public ResponseData(ResponseData response, ResponseStatusCode code)
        {
            StatusCode = HttpContextHelper.GetStatusCode(code);
            Result = response.Result;
            Error = response.Error;
        }

        public ResponseData(string path, ResponseStatusCode code)
        {
            StatusCode = HttpContextHelper.GetStatusCode(code);

            switch (StatusCode)
            {
                case 200:
                case 201:
                case 202:
                    Result = new { path = path };
                    break;
                default:
                    Error = new { code = (int)code, message = path };
                    break;
            }
        }

        public ResponseData(ResponseStatusCode code)
        {
            HttpContextHelper.SetStatusCode(code);
            StatusCode = HttpContextHelper.GetStatusCode(code);
            Result = null;

            switch (StatusCode)
            {
                case 401:
                    Error = new { code = StatusCode, message = "Доступ запрещен (Unauthorized)" };
                    break;

                case 400:
                    Error = new { code = StatusCode, message = "Проверьте правильность передаюмые параметры" };
                    break;

                case 403:
                    Error = new { code = StatusCode, message = "Нет доступ" };
                    break;
            }

        }
    }
}
