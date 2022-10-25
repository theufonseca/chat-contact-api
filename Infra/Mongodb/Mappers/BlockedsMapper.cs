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
    public class BlockedsMapper : AutoMapper.Profile
    {
        public BlockedsMapper()
        {
            CreateMap<BlockedsModel, Blockeds>();
            CreateMap<Blockeds, BlockedsModel>();
        }
    }
}
