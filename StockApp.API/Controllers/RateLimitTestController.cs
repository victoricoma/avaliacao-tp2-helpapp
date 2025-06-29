using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador para testar o Rate Limiting
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RateLimitTestController : ControllerBase
    {
        /// <summary>
        /// Endpoint de teste para operações de leitura (GET)
        /// Rate Limit: 200 requisições por minuto
        /// </summary>
        /// <returns>Mensagem de teste</returns>
        /// <response code="200">Requisição bem-sucedida</response>
        /// <response code="429">Rate limit excedido</response>
        [HttpGet("read-test")]
        public IActionResult ReadTest()
        {
            return Ok(new 
            { 
                message = "Teste de operação de leitura",
                timestamp = DateTime.UtcNow,
                rateLimit = "200 requisições por minuto"
            });
        }

        /// <summary>
        /// Endpoint de teste para operações de escrita (POST)
        /// Rate Limit: 50 requisições por minuto
        /// </summary>
        /// <param name="data">Dados de teste</param>
        /// <returns>Mensagem de teste</returns>
        /// <response code="200">Requisição bem-sucedida</response>
        /// <response code="429">Rate limit excedido</response>
        [HttpPost("write-test")]
        public IActionResult WriteTest([FromBody] object data)
        {
            return Ok(new 
            { 
                message = "Teste de operação de escrita",
                timestamp = DateTime.UtcNow,
                receivedData = data,
                rateLimit = "50 requisições por minuto"
            });
        }

        /// <summary>
        /// Endpoint de teste para simular autenticação
        /// Rate Limit: 10 requisições por 5 minutos
        /// </summary>
        /// <returns>Mensagem de teste</returns>
        /// <response code="200">Requisição bem-sucedida</response>
        /// <response code="429">Rate limit excedido</response>
        [HttpPost("auth-test")]
        public IActionResult AuthTest()
        {
            return Ok(new 
            { 
                message = "Teste de endpoint de autenticação",
                timestamp = DateTime.UtcNow,
                rateLimit = "10 requisições por 5 minutos"
            });
        }

        /// <summary>
        /// Endpoint protegido para testar Rate Limiting com autenticação
        /// Rate Limit: 200 requisições por minuto (operação de leitura)
        /// </summary>
        /// <returns>Mensagem de teste</returns>
        /// <response code="200">Requisição bem-sucedida</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="429">Rate limit excedido</response>
        [HttpGet("protected-test")]
        [Authorize]
        public IActionResult ProtectedTest()
        {
            return Ok(new 
            { 
                message = "Teste de endpoint protegido com Rate Limiting",
                timestamp = DateTime.UtcNow,
                user = User.Identity?.Name ?? "Usuário anônimo",
                rateLimit = "200 requisições por minuto"
            });
        }

        /// <summary>
        /// Endpoint para verificar informações do Rate Limiting
        /// </summary>
        /// <returns>Informações sobre as políticas de Rate Limiting</returns>
        [HttpGet("info")]
        public IActionResult GetRateLimitInfo()
        {
            var rateLimitInfo = new
            {
                policies = new
                {
                    defaultPolicy = new { maxRequests = 100, windowMinutes = 1 },
                    readOperations = new { maxRequests = 200, windowMinutes = 1 },
                    writeOperations = new { maxRequests = 50, windowMinutes = 1 },
                    authEndpoints = new { maxRequests = 10, windowMinutes = 5 }
                },
                headers = new
                {
                    limit = "X-RateLimit-Limit",
                    remaining = "X-RateLimit-Remaining",
                    reset = "X-RateLimit-Reset"
                },
                testEndpoints = new
                {
                    readTest = "/api/RateLimitTest/read-test",
                    writeTest = "/api/RateLimitTest/write-test",
                    authTest = "/api/RateLimitTest/auth-test",
                    protectedTest = "/api/RateLimitTest/protected-test"
                }
            };

            return Ok(rateLimitInfo);
        }
    }
}