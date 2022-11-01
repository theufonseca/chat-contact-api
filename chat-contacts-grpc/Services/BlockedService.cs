using Domain.UseCases.Blockeds;
using Google.Protobuf.Collections;
using Grpc.Core;
using MediatR;

namespace chat_contacts_grpc.Services
{
    public class BlockedService : Blocked.BlockedBase
    {
        private readonly IMediator mediator;

        public BlockedService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task<getBlockedListResponse> getBlockedList(getBlockedListRequest request, ServerCallContext context)
        {
            var response = new getBlockedListResponse();
            var result = await mediator.Send(new GetBlockedListRequest { Id = request.Id });

            if (result is null || result.Blockeds is null || result.Blockeds.BlockedList is null)
                return new getBlockedListResponse();

            foreach (var item in result.Blockeds.BlockedList)
            {
                response.BlockedList.Add(new getBlockedList
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    Nick = item.Nick,
                });
            }

            return response;
        }
    }
}
