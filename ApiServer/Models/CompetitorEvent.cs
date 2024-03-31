using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApiServer.Models
{
    public partial class CompetitorEvent
    {
        [ForeignKey("Event")]
        public long? EventId { get; set; }
        [ForeignKey("GamesCompetitor")]
        public long? CompetitorId { get; set; }
        [ForeignKey("Medal")]
        public long? MedalId { get; set; }

        public virtual GamesCompetitor Competitor { get; set; }
        public virtual Event Event { get; set; }
        public virtual Medal Medal { get; set; }
    }
}
