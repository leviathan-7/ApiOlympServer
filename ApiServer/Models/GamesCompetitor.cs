using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApiServer.Models
{
    public partial class GamesCompetitor
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Game")]
        public long? GamesId { get; set; }
        [ForeignKey("Person")]
        public long? PersonId { get; set; }
        public long? Age { get; set; }

        public virtual Game Games { get; set; }
        public virtual Person Person { get; set; }
    }
}
