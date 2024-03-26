using System;
using System.Collections.Generic;

#nullable disable

namespace ApiServer.Models
{
    public partial class PersonRegion
    {
        public long? PersonId { get; set; }
        public long? RegionId { get; set; }

        public virtual Person Person { get; set; }
        public virtual NocRegion Region { get; set; }
    }
}
