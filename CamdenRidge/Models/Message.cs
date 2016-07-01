using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamdenRidge.Models
{
    public class Message
    {
        public int ID { get; set; }

        public string Author { get; set; }

        public DateTime PostDate { get; set; }

        public string Text { get; set; }

        public bool Display { get; set; }

        public int ThreadID { get; set; }

        public virtual Thread Thread { get; set; }
    }
}