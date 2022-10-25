using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBlockedsService
    {
        Task<Blockeds> GetBlockeds(string id);
        Task Create(string id);
        Task Add(string id, Profile profile);
        Task Remove(string id, string friendId);
    }
}
