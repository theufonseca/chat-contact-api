using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.Blockeds
{
    public class RemoveBlockedRequest : IRequest<RemoveBlockedRequestResponse>
    {
        public string Id { get; init; } = default!;
        public string FriendId { get; init; } = default!;
    }

    public class RemoveBlockedRequestResponse
    {
        public bool Sucess { get; init; } = default!;
    }


    public class RemoveBlockedRequestHandler : IRequestHandler<RemoveBlockedRequest, RemoveBlockedRequestResponse>
    {
        private readonly IBlockedsService _blockedsService;

        public RemoveBlockedRequestHandler(IBlockedsService blockedsService)
        {
            _blockedsService = blockedsService;
        }

        public async Task<RemoveBlockedRequestResponse> Handle(RemoveBlockedRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
                throw new ArgumentException("Id precisa ser diferente de nulo ou de vazio");

            if (string.IsNullOrEmpty(request.FriendId))
                throw new ArgumentException("FriendId precisa ser diferente de nulo ou de vazio");

            await _blockedsService.Remove(request.Id, request.FriendId);

            return new RemoveBlockedRequestResponse { Sucess = true };
        }
    }
}
