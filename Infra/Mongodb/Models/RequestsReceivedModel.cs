using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mongodb.Models
{
    public class RequestsReceivedModel
    {
        [BsonId]
        public string Id { get; set; } = default!;
        public List<ProfileModel> RequestReceivedList { get; set; } = default!;
    }
}
