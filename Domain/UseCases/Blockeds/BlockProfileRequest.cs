using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.Blockeds
{
    public class BlockProfileRequest : IRequest<BlockProfileRequestResponse>
    {
        public string Id { get; init; } = default!;
        public string FriendId { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string Nick { get; init; } = default!;
        public string Email { get; init; } = default!;
    }

    public class BlockProfileRequestResponse
    {
        public bool Sucess { get; init; } = default!;
    }

    public class BlockProfileRequestHandler : IRequestHandler<BlockProfileRequest, BlockProfileRequestResponse>
    {
        private readonly IBlockedsService _blockedService;
        public BlockProfileRequestHandler(IBlockedsService blockedsService)
        {
            _blockedService = blockedsService;
        }

        public async Task<BlockProfileRequestResponse> Handle(BlockProfileRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
                throw new ArgumentException("Id precisa ser diferente de nulo e de vazio");

            if (string.IsNullOrEmpty(request.FriendId))
                throw new ArgumentException("FriendId precisa ser diferente de nulo e de vazio");

            var blockeds = await _blockedService.GetBlockeds(request.Id);

            if (Exists(blockeds, request.FriendId))
                return new BlockProfileRequestResponse { Sucess = true };

            var profile = new Profile
            {
                Id = request.FriendId,
                Email = request.Email,
                Name = request.Name,
                Nick = request.Nick
            };

            await _blockedService.Add(request.Id, profile);

            return new BlockProfileRequestResponse { Sucess = true };
        }

        private bool Exists(Entities.Blockeds blockeds, string friendId)
        {
            if (blockeds is null || !blockeds.BlockedList.Any(x => x.Id == friendId))
                return false;

            return true;
        }
    }
}
