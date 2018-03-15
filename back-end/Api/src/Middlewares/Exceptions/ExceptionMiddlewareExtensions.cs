using Microsoft.AspNetCore.Builder;


namespace Api.Middlewares.Exceptions {
    public static class ExceptionMiddlewareExtensions {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }

}