using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.Blockeds
{
    public class GetBlockedListRequest : IRequest<GetBlockedListRequestResponse>
    {
        public string Id { get; init; } = default!;
    }

    public class GetBlockedListRequestResponse
    {
        public Entities.Blockeds Blockeds { get; init; } = default!;
    }

    public class GetBlockedListRequestHandler : IRequestHandler<GetBlockedListRequest, GetBlockedListRequestResponse>
    {
        private readonly IBlockedsService _blockedsService;

        public GetBlockedListRequestHandler(IBlockedsService blockedsService)
        {
            _blockedsService = blockedsService;
        }

        public async Task<GetBlockedListRequestResponse> Handle(GetBlockedListRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
                throw new ArgumentException("Id precisa ser diferente de vazio e de nulo");

            var blockeds = await _blockedsService.GetBlockeds(request.Id);

            return new GetBlockedListRequestResponse
            {
                Blockeds = blockeds
            };
        }
    }
}
