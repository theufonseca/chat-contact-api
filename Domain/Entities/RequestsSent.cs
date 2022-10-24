using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RequestsSent
    {
        public string Id { get; set; } = default!;
        public List<Profile> RequestSentList { get; set; } = default!;
    }
}