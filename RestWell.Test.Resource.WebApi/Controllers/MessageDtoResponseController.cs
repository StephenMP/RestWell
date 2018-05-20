using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestWell.Test.Resource.WebApi.Dtos;

namespace RestWell.Test.Resource.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MessageDtoResponseRequestController : Controller
    {
        #region Public Methods

        [HttpDelete("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestDelete(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var responseDto = new MessageResponseDto { Message = message };
            return await Task.FromResult(Ok(responseDto));
        }

        [HttpGet("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestGet(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var responseDto = new MessageResponseDto { Message = message };
            return await Task.FromResult(Ok(responseDto));
        }

        [HttpHead("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestHead(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            return await Task.FromResult(Ok());
        }

        [HttpOptions("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestOptions(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var responseDto = new MessageResponseDto { Message = message };
            return await Task.FromResult(Ok(responseDto));
        }

        [HttpPatch("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestPatch(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var responseDto = new MessageResponseDto { Message = message };
            return await Task.FromResult(Ok(responseDto));
        }

        [HttpPost("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestPost(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var responseDto = new MessageResponseDto { Message = message };
            return await Task.FromResult(Ok(responseDto));
        }

        [HttpPut("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestPut(string message)
        {
            if (message == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var responseDto = new MessageResponseDto { Message = message };
            return await Task.FromResult(Ok(responseDto));
        }

        #endregion Public Methods
    }
}
