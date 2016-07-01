using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamdenRidge.Models
{
    public class Thread
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public ICollection<Message> Messages { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime LastPost { get; set; }
    }
}