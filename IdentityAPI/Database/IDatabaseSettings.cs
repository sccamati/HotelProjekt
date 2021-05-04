using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Database
{
    interface IDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string CollectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
