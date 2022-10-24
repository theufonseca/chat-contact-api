using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.Friends
{
    public class NewFriendRequest : IRequest<NewFriendRequestResponse>
    {
        public string Id { get; init; } = default!;
        public string FriendId { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string Nick { get; init; } = default!;
        public string Email { get; init; } = default!;
    }

    public class NewFriendRequestResponse
    {
        public bool Sucess { get; set; }
    }

    public class NewFriendRequestResponseHandler : IRequestHandler<NewFriendRequest, NewFriendRequestResponse>
    {
        private readonly IFriendsService _friendsService;

        public NewFriendRequestResponseHandler(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        public async Task<NewFriendRequestResponse> Handle(NewFriendRequest request, CancellationToken cancellationToken)
        {
            var friendProfile = new Profile
            {
                Id = request.FriendId,
                Name = request.Name,    
                Email = request.Email,
                Nick = request.Nick
            };

            if (string.IsNullOrEmpty(request.Id))
                throw new ArgumentException("Id precisa ser diferente de nulo e de vazio");

            if(string.IsNullOrEmpty(request.FriendId))
                throw new ArgumentException("FriendId precisa ser diferente de nulo e de vazio");

            var friends = await _friendsService.GetFriends(request.Id);

            if (friends is null)
                await _friendsService.Create(request.Id);

            await _friendsService.Add(request.Id, friendProfile);

            return new NewFriendRequestResponse
            {
                Sucess = true
            };
        }
    }
}