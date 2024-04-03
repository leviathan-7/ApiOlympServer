using System;
using System.Collections.Generic;

#nullable disable

namespace ApiServer.Models
{
    public partial class GamesCompetitor
    {
        public long Id { get; set; }
        public long? GamesId { get; set; }
        public long? PersonId { get; set; }
        public long? Age { get; set; }

        public virtual Game Games { get; set; }
        public virtual Person Person { get; set; }
    }
}
