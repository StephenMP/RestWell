using Microsoft.AspNetCore.Mvc;
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
            return await Task.FromResult(Ok(message));
        }

        [HttpGet("{message}")]
        public async Task<IActionResult> BasicGet(string message)
        {
            return await Task.FromResult(Ok(message));
        }

        [HttpHead("{message}")]
        public async Task<IActionResult> BasicHead(string message)
        {
            return await Task.FromResult(Ok());
        }

        [HttpOptions("{message}")]
        public async Task<IActionResult> BasicOptions(string message)
        {
            return await Task.FromResult(Ok(message));
        }

        [HttpPatch("{message}")]
        public async Task<IActionResult> BasicPatch(string message)
        {
            return await Task.FromResult(Ok(message));
        }

        [HttpPost("{message}")]
        public async Task<IActionResult> BasicPost(string message)
        {
            return await Task.FromResult(Ok(message));
        }

        [HttpPut("{message}")]
        public async Task<IActionResult> BasicPut(string message)
        {
            return await Task.FromResult(Ok(message));
        }

        #endregion Public Methods
    }
}
