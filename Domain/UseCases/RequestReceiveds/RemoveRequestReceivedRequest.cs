using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.RequestReceiveds
{
    public class RemoveRequestReceivedRequest : IRequest<RemoveRequestReceivedRequestResponse>
    {
        public string Id { get; init; } = default!;
        public string FriendId { get; set; } = default!;
    }

    public class RemoveRequestReceivedRequestResponse
    {
        public bool Sucess { get; init; } = default!;
    }

    public class RemoveRequestReceivedRequestHandler : IRequestHandler<RemoveRequestReceivedRequest, RemoveRequestReceivedRequestResponse>
    {
        private readonly IRequestsReceivedService _requestsReceivedService;

        public RemoveRequestReceivedRequestHandler(IRequestsReceivedService requestsReceivedService)
        {
            _requestsReceivedService = requestsReceivedService;
        }

        public async Task<RemoveRequestReceivedRequestResponse> Handle(RemoveRequestReceivedRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
                throw new ArgumentException("Id precisa ser diferente de nulo e de vazio");

            if (string.IsNullOrEmpty(request.FriendId))
                throw new ArgumentException("Id precisa ser diferente de nulo e de vazio");

            await _requestsReceivedService.Delete(request.Id, request.FriendId);

            return new RemoveRequestReceivedRequestResponse { Sucess = true };
        }
    }
}
