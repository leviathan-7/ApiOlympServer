using System;
using System.Collections.Generic;

#nullable disable

namespace ApiServer.Models
{
    public partial class Event
    {
        public long Id { get; set; }
        public long? SportId { get; set; }
        public string EventName { get; set; }

        public virtual Sport Sport { get; set; }
    }
}
