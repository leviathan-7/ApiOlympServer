using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ApiServer.Models
{
    public partial class NocRegion
    {
        [Key]
        public long Id { get; set; }
        public string Noc { get; set; }
        public string RegionName { get; set; }
    }
}
