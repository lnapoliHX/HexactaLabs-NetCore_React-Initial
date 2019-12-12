using Microsoft.AspNetCore.Builder;

namespace Stock.Api.Exceptions
{
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {   
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
