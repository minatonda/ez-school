using System;
using System.Threading.Tasks;
using Api.Common.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Configuration.Exceptions {

    public class ExceptionMiddleware {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory) {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory?.CreateLogger<ExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context) {
            context.Response.ContentType = @"application/json";
            try {
                await _next(context);
            } catch (BaseException ex) {
                context.Response.StatusCode = (int)ex.HttpStatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(BaseExceptionAdapter.ToBaseExceptionViewModel(ex)));
                return;
            }
        }

    }

}