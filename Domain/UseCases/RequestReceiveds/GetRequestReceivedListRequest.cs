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
    public class GetRequestReceivedListRequest : IRequest<GetRequestReceivedListRequestResponse>
    {
        public string Id { get; init; } = default!;
    }

    public class GetRequestReceivedListRequestResponse
    {
        public RequestsReceiveds requestsReceiveds { get; init; } = default!;
    }

    public class GetRequestReceivedListRequestHandler : IRequestHandler<GetRequestReceivedListRequest, GetRequestReceivedListRequestResponse>
    {
        private readonly IRequestsReceivedService _requestsReceivedService;

        public GetRequestReceivedListRequestHandler(IRequestsReceivedService requestsReceivedService)
        {
            _requestsReceivedService = requestsReceivedService;
        }

        public async Task<GetRequestReceivedListRequestResponse> Handle(GetRequestReceivedListRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
                throw new ArgumentException("Id precisa ser diferente de nulo e de vazio");

            var result = await _requestsReceivedService.Get(request.Id);

            return new GetRequestReceivedListRequestResponse
            {
                requestsReceiveds = result
            };
        }
    }
}
