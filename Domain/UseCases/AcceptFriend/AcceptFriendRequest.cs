using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.AcceptFriend
{
    public class AcceptFriendRequest : IRequest<AcceptFriendRequestResponse>
    {
        public string Id { get; init; } = default!;
        public string FriendId { get; set; } = default!;
    }

    public class AcceptFriendRequestResponse
    {
        public bool Sucess { get; init; } = default!;
    }

    public class AcceptFriendRequestHandler : IRequestHandler<AcceptFriendRequest, AcceptFriendRequestResponse>
    {
        private readonly IFriendsService _friendsService;
        private readonly IBlockedsService _blockedsService;
        private readonly IRequestsReceivedService _requestsReceivedService;

        public AcceptFriendRequestHandler(IFriendsService friendsService, IBlockedsService blockedsService, IRequestsReceivedService requestsReceivedService)
        {
            _friendsService = friendsService;
            _blockedsService = blockedsService;
            _requestsReceivedService = requestsReceivedService;
        }

        public async Task<AcceptFriendRequestResponse> Handle(AcceptFriendRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
                throw new ArgumentException("Id precisa ser diferente de nulo e de vazio");

            if (string.IsNullOrEmpty(request.FriendId))
                throw new ArgumentException("FriendId precisa ser diferente de nulo e de vazio");

            var requestsReceived = await _requestsReceivedService.Get(request.Id);
            var friend = requestsReceived.RequestReceivedList.FirstOrDefault(x => x.Id == request.FriendId);

            if (friend is null)
                throw new ArgumentException("Solicitação de Amizade não foi localizada");

            var friends = await _friendsService.GetFriends(request.Id);

            if (friends.FriendList.Any(x => x.Id == request.FriendId))
                throw new ArgumentException("Amigo já está na sua lista");

            var removeTask = _blockedsService.Remove(request.Id, request.FriendId);
            var addFriendTask = _friendsService.Add(request.Id, friend);

            await Task.WhenAll(removeTask, addFriendTask);

            await _requestsReceivedService.Delete(request.Id, request.FriendId);

            return new AcceptFriendRequestResponse { Sucess = true };
        }
    }
}
