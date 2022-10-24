using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mongodb.Models
{
    public class ProfileModel
    {
        public string Id { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string Nick { get; init; } = default!;
        public string Email { get; init; } = default!;
    }
}
