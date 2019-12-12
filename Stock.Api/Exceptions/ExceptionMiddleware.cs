using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Stock.Model.Exceptions;
using Stock.Repository.LiteDb.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Stock.Api.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (ModelException ex)
            {
                logger.LogError($"Something went wrong: {ex}");
                await HandleModelExceptionAsync(httpContext, ex);
            }
            catch (RepositoryException ex)
            {
                logger.LogError($"Something went wrong: {ex}");
                await HandleRepositoryExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleModelExceptionAsync(HttpContext context, ModelException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }

        private static Task HandleRepositoryExceptionAsync(HttpContext context, RepositoryException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error"
                }.ToString());
        }
    }
}
