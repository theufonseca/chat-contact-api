using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRequestsReceivedService
    {
        Task Add(string id, Profile profile);
        Task<RequestsReceiveds> Get(string id);
        Task Delete(string id, string friendId);
    }
}
