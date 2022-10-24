using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mongodb
{
    public class MongoDbConfig
    {
        public string ConnectionString { get; init; } = default!;
        public string DatabaseName { get; init; } = default!;
    }
}
