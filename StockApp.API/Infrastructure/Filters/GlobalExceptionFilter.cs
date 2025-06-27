using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


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
            context.Result = new ObjectResult(new
            {
                Error = "Erro interno. Tente novamente mais tarde.",
                context.Exception.Message
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            
            context.ExceptionHandled = true;
        }
    }
}

