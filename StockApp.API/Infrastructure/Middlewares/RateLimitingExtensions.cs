using StockApp.API.Infrastructure.Middlewares;

namespace StockApp.API.Infrastructure.Middlewares
{
    public static class RateLimitingExtensions
    {
        public static IServiceCollection AddRateLimiting(this IServiceCollection services, 
            Action<RateLimitOptions>? configureOptions = null)
        {
            var options = new RateLimitOptions();
            configureOptions?.Invoke(options);
            
            services.AddSingleton(options);
            services.AddMemoryCache();
            
            return services;
        }

        public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RateLimitingMiddleware>();
        }
    }
}