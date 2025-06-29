using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;
using Serilog;
using Serilog.Context;

namespace StockApp.API.Infrastructure.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly ILogger<RateLimitingMiddleware> _logger;
        private readonly RateLimitOptions _options;

        public RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache, 
            ILogger<RateLimitingMiddleware> logger, RateLimitOptions options)
        {
            _next = next;
            _cache = cache;
            _logger = logger;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            var clientId = GetClientIdentifier(context);
            var endpoint = GetEndpointIdentifier(context);
            var cacheKey = $"rate_limit_{clientId}_{endpoint}";

            var policy = GetRateLimitPolicy(context);
            
            if (!_cache.TryGetValue(cacheKey, out RequestCounter counter))
            {
                counter = new RequestCounter
                {
                    Count = 0,
                    WindowStart = DateTime.UtcNow
                };
            }

            // Verificar se a janela de tempo expirou
            if (DateTime.UtcNow - counter.WindowStart > policy.Window)
            {
                counter.Count = 0;
                counter.WindowStart = DateTime.UtcNow;
            }

            counter.Count++;

            // Armazenar no cache
            _cache.Set(cacheKey, counter, policy.Window);

            // Verificar se excedeu o limite
            if (counter.Count > policy.MaxRequests)
            {
                using (LogContext.PushProperty("ClientId", clientId))
                using (LogContext.PushProperty("Endpoint", endpoint))
                using (LogContext.PushProperty("RequestCount", counter.Count))
                using (LogContext.PushProperty("MaxRequests", policy.MaxRequests))
                {
                    Log.Warning("Rate limit excedido para cliente {ClientId} no endpoint {Endpoint}. " +
                               "Requisições: {RequestCount}/{MaxRequests}", 
                               clientId, endpoint, counter.Count, policy.MaxRequests);
                    
                    _logger.LogWarning("Rate limit excedido para cliente {ClientId} no endpoint {Endpoint}. " +
                                      "Requisições: {RequestCount}/{MaxRequests}", 
                                      clientId, endpoint, counter.Count, policy.MaxRequests);
                }

                await HandleRateLimitExceeded(context, policy);
                return;
            }

            // Adicionar headers informativos
            context.Response.Headers.Add("X-RateLimit-Limit", policy.MaxRequests.ToString());
            context.Response.Headers.Add("X-RateLimit-Remaining", (policy.MaxRequests - counter.Count).ToString());
            context.Response.Headers.Add("X-RateLimit-Reset", 
                ((DateTimeOffset)(counter.WindowStart.Add(policy.Window))).ToUnixTimeSeconds().ToString());

            await _next(context);
        }

        private string GetClientIdentifier(HttpContext context)
        {
            // Priorizar IP real em caso de proxy
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                return forwardedFor.Split(',')[0].Trim();
            }

            var realIp = context.Request.Headers["X-Real-IP"].FirstOrDefault();
            if (!string.IsNullOrEmpty(realIp))
            {
                return realIp;
            }

            return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        }

        private string GetEndpointIdentifier(HttpContext context)
        {
            return $"{context.Request.Method}:{context.Request.Path}";
        }

        private RateLimitPolicy GetRateLimitPolicy(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower() ?? "";
            var method = context.Request.Method.ToUpper();

            // Políticas específicas por endpoint
            if (path.Contains("/api/token") || path.Contains("/api/users/register"))
            {
                return _options.AuthEndpointsPolicy;
            }

            if (method == "POST" || method == "PUT" || method == "DELETE")
            {
                return _options.WriteOperationsPolicy;
            }

            if (method == "GET")
            {
                return _options.ReadOperationsPolicy;
            }

            return _options.DefaultPolicy;
        }

        private async Task HandleRateLimitExceeded(HttpContext context, RateLimitPolicy policy)
        {
            context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = "Rate limit exceeded",
                message = $"Muitas requisições. Limite: {policy.MaxRequests} por {policy.Window.TotalMinutes} minuto(s)",
                retryAfter = policy.Window.TotalSeconds
            };

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }

    public class RequestCounter
    {
        public int Count { get; set; }
        public DateTime WindowStart { get; set; }
    }

    public class RateLimitPolicy
    {
        public int MaxRequests { get; set; }
        public TimeSpan Window { get; set; }
    }

    public class RateLimitOptions
    {
        public RateLimitPolicy DefaultPolicy { get; set; } = new()
        {
            MaxRequests = 100,
            Window = TimeSpan.FromMinutes(1)
        };

        public RateLimitPolicy ReadOperationsPolicy { get; set; } = new()
        {
            MaxRequests = 200,
            Window = TimeSpan.FromMinutes(1)
        };

        public RateLimitPolicy WriteOperationsPolicy { get; set; } = new()
        {
            MaxRequests = 50,
            Window = TimeSpan.FromMinutes(1)
        };

        public RateLimitPolicy AuthEndpointsPolicy { get; set; } = new()
        {
            MaxRequests = 10,
            Window = TimeSpan.FromMinutes(5)
        };
    }
}