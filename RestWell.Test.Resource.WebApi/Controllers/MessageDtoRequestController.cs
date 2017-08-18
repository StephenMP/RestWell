using Microsoft.AspNetCore.Mvc;
using RestWell.Test.Resource.WebApi.Dtos;

namespace RestWell.Test.Resource.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MessageDtoRequestController : Controller
    {
        [HttpPost]
        public IActionResult MessageDtoPost([FromBody]MessageRequestDto messageRequestDto)
        {
            var responseDto = new MessageResponseDto { Message = messageRequestDto.Message };
            return Ok(responseDto);
        }

        [HttpPut]
        public IActionResult MessageDtoPut([FromBody]MessageRequestDto messageRequestDto)
        {
            var responseDto = new MessageResponseDto { Message = messageRequestDto.Message };
            return Ok(responseDto);
        }

        [HttpPatch]
        public IActionResult MessageDtoPatch([FromBody]MessageRequestDto messageRequestDto)
        {
            var responseDto = new MessageResponseDto { Message = messageRequestDto.Message };
            return Ok(responseDto);
        }
    }
}
