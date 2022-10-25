using AutoMapper;
using Domain.Entities;
using Infra.Mongodb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mongodb.Mappers
{
    public class RequestsReceivedMapper : AutoMapper.Profile
    {
        public RequestsReceivedMapper()
        {
            CreateMap<RequestsReceiveds, RequestsReceivedModel>();
            CreateMap<RequestsReceivedModel, RequestsReceiveds>();
        }
    }
}
