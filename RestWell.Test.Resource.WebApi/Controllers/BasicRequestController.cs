using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace RestWell.Test.Resource.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BasicRequestController : Controller
    {
        #region Public Methods

        [HttpDelete("{message}")]
        public async Task<IActionResult> BasicDelete(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            return await Task.FromResult(Ok(message));
        }

        [HttpGet("{message}")]
        public async Task<IActionResult> BasicGet(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            return await Task.FromResult(Ok(message));
        }

        [HttpHead("{message}")]
        public async Task<IActionResult> BasicHead(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            return await Task.FromResult(Ok());
        }

        [HttpOptions("{message}")]
        public async Task<IActionResult> BasicOptions(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            return await Task.FromResult(Ok(message));
        }

        [HttpPatch("{message}")]
        public async Task<IActionResult> BasicPatch(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            return await Task.FromResult(Ok(message));
        }

        [HttpPost("{message}")]
        public async Task<IActionResult> BasicPost(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            return await Task.FromResult(Ok(message));
        }

        [HttpPut("{message}")]
        public async Task<IActionResult> BasicPut(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            return await Task.FromResult(Ok(message));
        }

        [HttpDelete("error/{message}")]
        public async Task<IActionResult> BasicDeleteError(string message)
        {
            var exception = new Exception(message);
            return await Task.FromResult(StatusCode(500, exception));
        }

        [HttpGet("error/{message}")]
        public async Task<IActionResult> BasicGetError(string message)
        {
            var exception = new Exception(message);
            return await Task.FromResult(StatusCode(500, exception));
        }

        [HttpHead("error/{message}")]
        public async Task<IActionResult> BasicHeadError(string message)
        {
            var exception = new Exception(message);
            return await Task.FromResult(StatusCode(500, exception));
        }

        [HttpOptions("error/{message}")]
        public async Task<IActionResult> BasicOptionsError(string message)
        {
            var exception = new Exception(message);
            return await Task.FromResult(StatusCode(500, exception));
        }

        [HttpPatch("error/{message}")]
        public async Task<IActionResult> BasicPatchError(string message)
        {
            var exception = new Exception(message);
            return await Task.FromResult(StatusCode(500, exception));
        }

        [HttpPost("error/{message}")]
        public async Task<IActionResult> BasicPostError(string message)
        {
            var exception = new Exception(message);
            return await Task.FromResult(StatusCode(500, exception));
        }

        [HttpPut("error/{message}")]
        public async Task<IActionResult> BasicPutError(string message)
        {
            var exception = new Exception(message);
            return await Task.FromResult(StatusCode(500, exception));
        }

        #endregion Public Methods
    }
}
