using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamdenRidge.Models
{
    public class Document
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Path { get; set; }

        public string Category { get; set; }

        public bool Display { get; set; }

        public bool Public { get; set; }

        public int Sequence { get; set; }
    }
}