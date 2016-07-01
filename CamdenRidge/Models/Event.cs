using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CamdenRidge.Models
{
    public class Event
    {
        public int ID { get; set; }

        [Required]
        public string Name
        {
            get; set;
        }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Body { get; set; }

        public string Location { get; set; }

        public bool Display { get; set; }

        [Display(Name="Board and Committee Members Only")]
        public bool BoardAndCommmitteeOnly { get; set; }
    }
}