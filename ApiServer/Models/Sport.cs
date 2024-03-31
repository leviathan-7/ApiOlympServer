using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ApiServer.Models
{
    public partial class Sport
    {
        public Sport()
        {
            Events = new HashSet<Event>();
        }
        [Key]
        public long Id { get; set; }
        public string SportName { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
