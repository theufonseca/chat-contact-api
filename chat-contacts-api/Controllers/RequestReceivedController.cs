using Domain.UseCases.AcceptFriend;
using Domain.UseCases.RequestReceiveds;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chat_contacts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestReceivedController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RequestReceivedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewRequestReceivedRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var request = new GetRequestReceivedListRequest { Id = id };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Put(RemoveRequestReceivedRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("/accept")]
        public async Task<IActionResult> AcceptFriend(AcceptFriendRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
