using Domain.Entities;
using Infra.Mongodb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mongodb.Mappers
{
    public class FriendsMapper : AutoMapper.Profile
    {
        public FriendsMapper()
        {
            CreateMap<Profile, ProfileModel>();
            CreateMap<ProfileModel, Profile>();
            CreateMap<Friends, FriendsModel>();
            CreateMap<FriendsModel, Friends>();
        }
    }
}
