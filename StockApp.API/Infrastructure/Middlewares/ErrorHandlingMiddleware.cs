using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Serilog;
using Serilog.Context;
using StockApp.Domain.Exceptions;

namespace StockApp.API.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                using (LogContext.PushProperty("RequestId", context.TraceIdentifier))
                using (LogContext.PushProperty("RequestPath", context.Request.Path))
                using (LogContext.PushProperty("RequestMethod", context.Request.Method))
                using (LogContext.PushProperty("UserAgent", context.Request.Headers["User-Agent"].FirstOrDefault()))
                using (LogContext.PushProperty("RemoteIP", context.Connection.RemoteIpAddress?.ToString()))
                {
                    Log.Error(ex, "Erro não tratado na requisição {RequestMethod} {RequestPath}", 
                        context.Request.Method, context.Request.Path);
                    
                    _logger.LogError(ex, "Erro não tratado na requisição {RequestMethod} {RequestPath}", 
                        context.Request.Method, context.Request.Path);
                }
                
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Erro interno. Tente novamente mais tarde.";
            
            if (exception is AuthenticationException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = "Erro de autenticação";
            }
            else if (exception is AuthorizationException)
            {
                statusCode = HttpStatusCode.Forbidden;
                message = "Erro de autorização";
            }
            
            context.Response.StatusCode = (int)statusCode;
            
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
                Detailed = exception.Message
            };
            
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
