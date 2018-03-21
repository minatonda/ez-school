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
            } catch (BaseFieldInvalidException ex) {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(BaseExceptionAdapter.ToBaseFieldInvalidExceptionViewModel(ex)));
            } catch (BaseUnauthorizedInstituicaoException ex) {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(BaseExceptionAdapter.ToBaseUnauthorizedInstituicaoException(ex)));
            } catch (BaseUnauthorizedException ex) {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(BaseExceptionAdapter.ToBaseUnauthorizedException(ex)));
            } catch (BaseException ex) {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(BaseExceptionAdapter.ToBaseExceptionViewModel(ex)));
                return;
            }
        }

    }

}