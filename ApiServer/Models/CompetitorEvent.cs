using System;
using System.Collections.Generic;

#nullable disable

namespace ApiServer.Models
{
    public partial class CompetitorEvent
    {
        public long? EventId { get; set; }
        public long? CompetitorId { get; set; }
        public long? MedalId { get; set; }

        public virtual GamesCompetitor Competitor { get; set; }
        public virtual Event Event { get; set; }
        public virtual Medal Medal { get; set; }
    }
}
