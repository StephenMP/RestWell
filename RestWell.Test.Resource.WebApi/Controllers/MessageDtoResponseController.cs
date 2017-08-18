using Microsoft.AspNetCore.Mvc;
using RestWell.Test.Resource.WebApi.Dtos;
using System.Threading.Tasks;

namespace RestWell.Test.Resource.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MessageDtoResponseRequestController : Controller
    {
        [HttpGet("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestGet(string message)
        {
            var responseDto = new MessageResponseDto { Message = message };
            return await Task.FromResult(Ok(responseDto));
        }

        [HttpPost("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestPost(string message)
        {
            var responseDto = new MessageResponseDto { Message = message };
            return await Task.FromResult(Ok(responseDto));
        }

        [HttpPut("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestPut(string message)
        {
            var responseDto = new MessageResponseDto { Message = message };
            return await Task.FromResult(Ok(responseDto));
        }

        [HttpDelete("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestDelete(string message)
        {
            var responseDto = new MessageResponseDto { Message = message };
            return await Task.FromResult(Ok(responseDto));
        }

        [HttpOptions("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestOptions(string message)
        {
            var responseDto = new MessageResponseDto { Message = message };
            return await Task.FromResult(Ok(responseDto));
        }

        [HttpPatch("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestPatch(string message)
        {
            var responseDto = new MessageResponseDto { Message = message };
            return await Task.FromResult(Ok(responseDto));
        }

        [HttpHead("{message}")]
        public async Task<IActionResult> MessageDtoResponseRequestHead(string message)
        {
            return await Task.FromResult(Ok());
        }
    }
}
