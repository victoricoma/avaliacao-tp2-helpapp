using Serilog;
using Serilog.Context;
using Serilog.Events;
using System.Diagnostics;
using System.Text;

namespace StockApp.API.Infrastructure.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var requestId = context.TraceIdentifier;
            
            // Capturar informações da requisição
            var requestInfo = new
            {
                RequestId = requestId,
                Method = context.Request.Method,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.ToString(),
                UserAgent = context.Request.Headers["User-Agent"].FirstOrDefault(),
                RemoteIP = context.Connection.RemoteIpAddress?.ToString(),
                ContentType = context.Request.ContentType,
                ContentLength = context.Request.ContentLength
            };

            using (LogContext.PushProperty("RequestId", requestId))
            using (LogContext.PushProperty("RequestMethod", requestInfo.Method))
            using (LogContext.PushProperty("RequestPath", requestInfo.Path))
            using (LogContext.PushProperty("UserAgent", requestInfo.UserAgent))
            using (LogContext.PushProperty("RemoteIP", requestInfo.RemoteIP))
            {
                Log.Information("Iniciando requisição {RequestMethod} {RequestPath}{QueryString}",
                    requestInfo.Method, requestInfo.Path, requestInfo.QueryString);

                try
                {
                    await _next(context);
                }
                finally
                {
                    stopwatch.Stop();
                    var elapsed = stopwatch.ElapsedMilliseconds;
                    var statusCode = context.Response.StatusCode;

                    var logLevel = statusCode >= 500 ? LogEventLevel.Error :
                                  statusCode >= 400 ? LogEventLevel.Warning :
                                  LogEventLevel.Information;

                    Log.Write(logLevel, "Requisição {RequestMethod} {RequestPath} finalizada com status {StatusCode} em {ElapsedMs}ms",
                        requestInfo.Method, requestInfo.Path, statusCode, elapsed);
                }
            }
        }
    }
}