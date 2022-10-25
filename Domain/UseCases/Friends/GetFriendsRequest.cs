using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.Friends
{
    public class GetFriendsRequest : IRequest<GetFriendsRequestResponse>
    {
        public string Id { get; init; } = default!;
    }
    
    public class GetFriendsRequestResponse
    {
        public Entities.Friends friends { get; init; } = default!;
    }

    public class GetFriendsRequestHandler : IRequestHandler<GetFriendsRequest, GetFriendsRequestResponse>
    {
        private readonly IFriendsService _friendsService;

        public GetFriendsRequestHandler(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        public async Task<GetFriendsRequestResponse> Handle(GetFriendsRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
                throw new ArgumentException("Id precisa ser diferente de nulo e de vazio");

            var friends = await _friendsService.GetFriends(request.Id);

            return new GetFriendsRequestResponse
            {
                friends = friends
            };
        }
    }
}
