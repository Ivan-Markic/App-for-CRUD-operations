using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserWebApp.Models
{
    public class SubCategoryDto
    {
        public int IDPotkategorija { get; set; }

        [Display(Name = "Potkategorija")]
        public string Naziv { get; set; }
    }
}