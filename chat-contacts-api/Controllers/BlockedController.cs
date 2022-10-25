using Domain.UseCases.Blockeds;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chat_contacts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockedController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public BlockedController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var request = new GetBlockedListRequest { Id = id };
            var response = await _mediatr.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BlockProfileRequest request)
        {
            var response = await _mediatr.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(RemoveBlockedRequest request)
        {
            var response = await _mediatr.Send(request);
            return Ok(response);
        }
    }
}
