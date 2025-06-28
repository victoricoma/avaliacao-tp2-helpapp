using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using StockApp.Domain.Exceptions;


namespace StockApp.API.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Erro capturado pelo filtro global");

            var statusCode = StatusCodes.Status500InternalServerError;
            var message = "Erro interno. Tente novamente mais tarde.";

            if (context.Exception is AuthenticationException)
            {
                statusCode = StatusCodes.Status401Unauthorized;
                message = "Erro de autenticação";
            }
            else if (context.Exception is AuthorizationException)
            {
                statusCode = StatusCodes.Status403Forbidden;
                message = "Erro de autorização";
            }

            context.Result = new ObjectResult(new
            {
                Error = message,
                context.Exception.Message
            })
            {
                StatusCode = statusCode
            };
            
            context.ExceptionHandled = true;
        }
    }
}

