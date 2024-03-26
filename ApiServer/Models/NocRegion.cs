using System;
using System.Collections.Generic;

#nullable disable

namespace ApiServer.Models
{
    public partial class NocRegion
    {
        public long Id { get; set; }
        public string Noc { get; set; }
        public string RegionName { get; set; }
    }
}
