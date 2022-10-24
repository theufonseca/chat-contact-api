using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFriendsService
    {
        Task Add(string id, Profile profile);
        Task Create(string id);
        Task<Friends> GetFriends(string id);
    }
}
