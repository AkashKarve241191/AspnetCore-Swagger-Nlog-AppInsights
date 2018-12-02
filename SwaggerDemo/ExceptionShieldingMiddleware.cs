using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace SwaggerDemo
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionShieldingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionShieldingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
               // throw new ArgumentException("Errorrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr!!!!!!!!!!!!");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionShieldingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionShieldingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionShieldingMiddleware>();
        }
    }
}
