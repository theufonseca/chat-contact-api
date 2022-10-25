using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.Friends
{
    public class RemoveFriendRequest : IRequest<RemoveFriendRequestResponse>
    {
        public string Id { get; init; } = default!;
        public string FriendId { get; init; } = default!;
    }

    public class RemoveFriendRequestResponse
    {
        public bool Sucess { get; set; }
    }

    public class RemoveFriendRequestHandler : IRequestHandler<RemoveFriendRequest, RemoveFriendRequestResponse>
    {
        private readonly IFriendsService _friendsService;

        public RemoveFriendRequestHandler(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        public async Task<RemoveFriendRequestResponse> Handle(RemoveFriendRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
                throw new ArgumentException("Id precisa ser diferente de nulo e de vazio");

            if (string.IsNullOrEmpty(request.FriendId))
                throw new ArgumentException("FriendId precisa ser diferente de nulo e de vazio");

            await _friendsService.Remove(request.Id, request.FriendId);

            return new RemoveFriendRequestResponse
            {
                Sucess = true
            };
        }
    }
}
