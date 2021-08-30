using Microsoft.AspNetCore.Http;
using QuotesAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesAPI.Helper
{
    public static class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor;

        public static HttpContext Current => Accessor?.HttpContext;

        public static void SetErrorInBodyCode()
        {
            SetStatusCode(ResponseStatusCode.ErrorInBody);
        }

        public static void SetStatusCode(ResponseStatusCode code)
        {
            var responseCode = (int)Enum.ToObject(code.GetType(), code);
            SetStatusCode(responseCode);
        }

        /// <summary>
        /// return code status
        /// </summary>
        /// <param name="code"></param>
        public static int GetStatusCode(ResponseStatusCode code)
        {
            var responseCode = (int)Enum.ToObject(code.GetType(), code);
            SetStatusCode(responseCode);
            return responseCode;
        }

        public static void SetStatusCode(int code)
        {
            if (Current?.Response != null)
                Current.Response.StatusCode = code;
        }
    }
}
