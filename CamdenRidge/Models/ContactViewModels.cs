using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CamdenRidge.Models
{
    public class ContactViewModel
    {
        [Required]
        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone")]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "How can we help you?")]
        public string Text { get; set; }

        [Display(Name = "Send To")]
        public string SendTo { get; set; }

        public List<SelectListItem> sendToItemTypes { get; set; }

        public bool Sent { get; set; }
    }
}