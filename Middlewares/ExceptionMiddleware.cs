using System.Net;
using System.Text.Json;

namespace App.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    statusCode = context.Response.StatusCode,
                    message = "Sunucu hatası oluştu",
                    detail = ex.Message
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response)
                );
            }
        }
    }
}
