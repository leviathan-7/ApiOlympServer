using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ApiServer.Models
{
    public partial class Medal
    {
        [Key]
        public long Id { get; set; }
        public string MedalName { get; set; }
    }
}
