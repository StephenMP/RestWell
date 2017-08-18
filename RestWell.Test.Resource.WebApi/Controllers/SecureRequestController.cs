using Microsoft.AspNetCore.Mvc;
using RestWell.Test.Resource.WebApi.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace RestWell.Test.Resource.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class SecureRequestController : Controller
    {
        [HttpGet("{message}")]
        public async Task<IActionResult> SecureRequestGet(string message)
        {
            var authHeaderIsPresent = Request.Headers.Any(h => h.Key == "Authorization");

            if (authHeaderIsPresent)
            {
                var authHeader = Request.Headers.First(h => h.Key == "Authorization");
                if (authHeader.Value == "Basic Username:Password")
                {
                    var responseDto = new MessageResponseDto { Message = message };
                    return await Task.FromResult(Ok(responseDto));
                }
            }

            return await Task.FromResult(Unauthorized());
        }

        [HttpPost("{message}")]
        public async Task<IActionResult> SecureRequestPost(string message)
        {
            var authHeaderIsPresent = Request.Headers.Any(h => h.Key == "Authorization");

            if (authHeaderIsPresent)
            {
                var authHeader = Request.Headers.First(h => h.Key == "Authorization");
                if (authHeader.Value == "Basic Username:Password")
                {
                    var responseDto = new MessageResponseDto { Message = message };
                    return await Task.FromResult(Ok(responseDto));
                }
            }

            return await Task.FromResult(Unauthorized());
        }

        [HttpPut("{message}")]
        public async Task<IActionResult> SecureRequestPut(string message)
        {
            var authHeaderIsPresent = Request.Headers.Any(h => h.Key == "Authorization");

            if (authHeaderIsPresent)
            {
                var authHeader = Request.Headers.First(h => h.Key == "Authorization");
                if (authHeader.Value == "Basic Username:Password")
                {
                    var responseDto = new MessageResponseDto { Message = message };
                    return await Task.FromResult(Ok(responseDto));
                }
            }

            return await Task.FromResult(Unauthorized());
        }

        [HttpDelete("{message}")]
        public async Task<IActionResult> SecureRequestDelete(string message)
        {
            var authHeaderIsPresent = Request.Headers.Any(h => h.Key == "Authorization");

            if (authHeaderIsPresent)
            {
                var authHeader = Request.Headers.First(h => h.Key == "Authorization");
                if (authHeader.Value == "Basic Username:Password")
                {
                    var responseDto = new MessageResponseDto { Message = message };
                    return await Task.FromResult(Ok(responseDto));
                }
            }

            return await Task.FromResult(Unauthorized());
        }

        [HttpOptions("{message}")]
        public async Task<IActionResult> SecureRequestOptions(string message)
        {
            var authHeaderIsPresent = Request.Headers.Any(h => h.Key == "Authorization");

            if (authHeaderIsPresent)
            {
                var authHeader = Request.Headers.First(h => h.Key == "Authorization");
                if (authHeader.Value == "Basic Username:Password")
                {
                    var responseDto = new MessageResponseDto { Message = message };
                    return await Task.FromResult(Ok(responseDto));
                }
            }

            return await Task.FromResult(Unauthorized());
        }

        [HttpPatch("{message}")]
        public async Task<IActionResult> SecureRequestPatch(string message)
        {
            var authHeaderIsPresent = Request.Headers.Any(h => h.Key == "Authorization");

            if (authHeaderIsPresent)
            {
                var authHeader = Request.Headers.First(h => h.Key == "Authorization");
                if (authHeader.Value == "Basic Username:Password")
                {
                    var responseDto = new MessageResponseDto { Message = message };
                    return await Task.FromResult(Ok(responseDto));
                }
            }

            return await Task.FromResult(Unauthorized());
        }

        [HttpHead("{message}")]
        public async Task<IActionResult> SecureRequestHead(string message)
        {
            var authHeaderIsPresent = Request.Headers.Any(h => h.Key == "Authorization");

            if (authHeaderIsPresent)
            {
                var authHeader = Request.Headers.First(h => h.Key == "Authorization");
                if (authHeader.Value == "Basic Username:Password")
                {
                    return await Task.FromResult(Ok());
                }
            }

            return await Task.FromResult(Unauthorized());
        }
    }
}
