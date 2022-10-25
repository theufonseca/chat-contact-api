using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.RequestReceiveds
{
    public class NewRequestReceivedRequest : IRequest<NewRequestReceivedRequestResponse>
    {
        public string Id { get; init; } = default!;
        public string FriendId { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string Nick { get; init; } = default!;
        public string Email { get; init; } = default!;
    }

    public class NewRequestReceivedRequestResponse
    {
        public bool Sucess { get; init; } = default!;
    }

    public class NewRequestReceivedRequestHandler : IRequestHandler<NewRequestReceivedRequest, NewRequestReceivedRequestResponse>
    {
        private readonly IRequestsReceivedService _requestReceivedService;

        public NewRequestReceivedRequestHandler(IRequestsReceivedService requestReceivedService)
        {
            _requestReceivedService = requestReceivedService;
        }

        public async Task<NewRequestReceivedRequestResponse> Handle(NewRequestReceivedRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
                throw new ArgumentException("Id precisa ser diferente de nulo e de vazio");

            if (string.IsNullOrWhiteSpace(request.FriendId))
                throw new ArgumentException("FriendId precisa ser diferente de nulo e de vazio");

            var requestsReceiveds = await _requestReceivedService.Get(request.Id);

            if(Exists(requestsReceiveds, request.FriendId))
                return new NewRequestReceivedRequestResponse { Sucess = true };

            var profile = new Profile
            {
                Id = request.FriendId,
                Email = request.Email,
                Name = request.Name,
                Nick = request.Nick
            };

            await _requestReceivedService.Add(request.Id, profile);

            return new NewRequestReceivedRequestResponse { Sucess = true };
        }

        public bool Exists(RequestsReceiveds requestsReceiveds, string friendId)
        {
            if(requestsReceiveds is null || !requestsReceiveds.RequestReceivedList.Any(x => x.Id == friendId))
                return false;

            return true;
        }
    }
}
