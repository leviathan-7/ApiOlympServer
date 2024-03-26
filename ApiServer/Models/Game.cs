using System;
using System.Collections.Generic;

#nullable disable

namespace ApiServer.Models
{
    public partial class Game
    {
        public Game()
        {
            GamesCompetitors = new HashSet<GamesCompetitor>();
        }

        public long Id { get; set; }
        public long? GamesYear { get; set; }
        public string GamesName { get; set; }
        public string Season { get; set; }

        public virtual ICollection<GamesCompetitor> GamesCompetitors { get; set; }
    }
}
