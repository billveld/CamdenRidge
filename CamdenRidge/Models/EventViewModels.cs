using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamdenRidge.Models
{
    public class EventViewModel
    {
        public bool ShowAdminControls { get; set; }

        public IEnumerable<Event> Events { get; set; }
    }
}