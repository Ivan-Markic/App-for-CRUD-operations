using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserWebApp.Models
{
    
    public class Customer
    {
        [Display(Name = "Kupac")]
        public int IDKupac { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public City Grad { get; set; }
        public override string ToString()
        => $"{Ime} {Prezime}";
    }
}