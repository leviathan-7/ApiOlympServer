using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ApiServer.Models
{
    public partial class Event
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Sport")]
        public long? SportId { get; set; }
        public string EventName { get; set; }

        public virtual Sport Sport { get; set; }
    }
}
