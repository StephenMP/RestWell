using Microsoft.AspNetCore.Mvc;
using RestWell.Test.Resource.WebApi.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace RestWell.Test.Resource.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class SecureRequestController : Controller
    {
        #region Public Methods

        [HttpDelete("{message}")]
        public async Task<IActionResult> SecureRequestDelete(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

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

        [HttpGet("{message}")]
        public async Task<IActionResult> SecureRequestGet(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

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
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

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

        [HttpOptions("{message}")]
        public async Task<IActionResult> SecureRequestOptions(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

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
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

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
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

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
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

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

        #endregion Public Methods
    }
}
