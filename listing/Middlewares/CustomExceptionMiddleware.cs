using Listing.Constants;
using Listing.Model;
using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Listing.Extensions
{
    public class CustomExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        static readonly ILogger Log = Serilog.Log.ForContext<CustomExceptionMiddleware>();
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Log.Error(ErrorMessages.GLOBAL_EXCEPTION_MESSAGE, ex);
                await HandleExceptionAsync(httpContext);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return httpContext.Response.WriteAsync(new Error
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ErrorMessages.INTERNAL_SERVER_ERROR
            }.ToString());
        }
    }
}
