using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Edu.Ntu.Foundation.Core.ErrorResponses;


namespace Edu.Ntu.Foundation.Core.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            string message = context.Exception.InnerException == null
                    ? context.Exception.Message : context.Exception.InnerException.Message;

            string errorCode = HttpStatusCode.InternalServerError.ToString();
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            if (context.Exception.GetType() == typeof(Exception))
            {
                var exception = (Exception)context.Exception;
                errorCode = exception == null ? HttpStatusCode.BadRequest.ToString() : exception.ToString();
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception.GetType() == typeof(ArgumentNullException) 
                || context.Exception.GetType() == typeof(ArgumentException)
                || context.Exception.GetType() == typeof(ArgumentOutOfRangeException)) 
            {
                errorCode = HttpStatusCode.BadRequest.ToString();
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            _logger.LogError(context.Exception, "exception message: " + message);

            var request = context.HttpContext.Request;
            request.EnableRewind();
            request.Body.Position = 0;

            var requestBodyObject = string.Empty;

            using (StreamReader reader = new StreamReader(request.Body))
            {
                requestBodyObject = reader.ReadToEnd().ToString();
            }

            var errorResponse = new ErrorResponse()
            {
                Code = errorCode,
                Message = message,
                Parameters = JsonConvert.DeserializeObject<object>(requestBodyObject)
            };

            context.Result = new JsonResult(errorResponse);
        }
    }
}