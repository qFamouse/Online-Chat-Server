using OnlineChat.Core.Exceptions;
using System.Net;

namespace OnlineChat.WebUI.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                await HandleHttpStatusCodeExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        #region Handlers

        private static Task HandleHttpStatusCodeExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            return context.Response.WriteAsJsonAsync(new
            {
                StatusCode = exception.StatusCode,
                Error = exception.Message
            });
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.StatusCode = statusCode;

            // TODO: Client shouldn't see error message and type. We can log it 
            return context.Response.WriteAsJsonAsync(new
            {
                StatusCode = statusCode,
                ErrorType = exception.GetType().FullName,
                Error = exception.Message
            });
        }

        #endregion
    }
}
