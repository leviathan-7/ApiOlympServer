using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApiServer.Models
{
    public partial class GamesCity
    {
        [ForeignKey("Game")]
        public long? GamesId { get; set; }
        [ForeignKey("City")]
        public long? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual Game Games { get; set; }
    }
}
