using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ApiServer.Models
{
    public partial class City
    {
        [Key]
        public long Id { get; set; }
        public string CityName { get; set; }
    }
}
