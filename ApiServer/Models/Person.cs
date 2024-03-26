using System;
using System.Collections.Generic;

#nullable disable

namespace ApiServer.Models
{
    public partial class Person
    {
        public Person()
        {
            GamesCompetitors = new HashSet<GamesCompetitor>();
        }

        public long Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public long? Height { get; set; }
        public long? Weight { get; set; }

        public virtual ICollection<GamesCompetitor> GamesCompetitors { get; set; }
    }
}
