using System;
using System.Collections.Generic;

#nullable disable

namespace ApiServer.Models
{
    public partial class GamesCity
    {
        public long? GamesId { get; set; }
        public long? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual Game Games { get; set; }
    }
}
