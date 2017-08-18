using Microsoft.AspNetCore.Mvc;
using RestWell.Test.Resource.WebApi.Dtos;

namespace RestWell.Test.Resource.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MessageDtoRequestController : Controller
    {
        #region Public Methods

        [HttpDelete]
        public IActionResult MessageDtoDelete([FromBody]MessageRequestDto messageRequestDto)
        {
            if (messageRequestDto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var responseDto = new MessageResponseDto { Message = messageRequestDto.Message };
            return Ok(responseDto);
        }

        [HttpGet]
        public IActionResult MessageDtoGet([FromBody]MessageRequestDto messageRequestDto)
        {
            if (messageRequestDto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var responseDto = new MessageResponseDto { Message = messageRequestDto.Message };
            return Ok(responseDto);
        }

        [HttpHead]
        public IActionResult MessageDtoHead([FromBody]MessageRequestDto messageRequestDto)
        {
            if (messageRequestDto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpOptions]
        public IActionResult MessageDtoOptions([FromBody]MessageRequestDto messageRequestDto)
        {
            if (messageRequestDto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var responseDto = new MessageResponseDto { Message = messageRequestDto.Message };
            return Ok(responseDto);
        }

        [HttpPatch]
        public IActionResult MessageDtoPatch([FromBody]MessageRequestDto messageRequestDto)
        {
            if (messageRequestDto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var responseDto = new MessageResponseDto { Message = messageRequestDto.Message };
            return Ok(responseDto);
        }

        [HttpPost]
        public IActionResult MessageDtoPost([FromBody]MessageRequestDto messageRequestDto)
        {
            if (messageRequestDto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var responseDto = new MessageResponseDto { Message = messageRequestDto.Message };
            return Ok(responseDto);
        }

        [HttpPut]
        public IActionResult MessageDtoPut([FromBody]MessageRequestDto messageRequestDto)
        {
            if (messageRequestDto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var responseDto = new MessageResponseDto { Message = messageRequestDto.Message };
            return Ok(responseDto);
        }

        #endregion Public Methods
    }
}
