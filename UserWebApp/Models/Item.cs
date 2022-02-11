using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserWebApp.Models
{
    public class Item
    {
        public int IDStavka { get; set; }
        [Display(Name = "Račun")]
        public Bill Racun { get; set; }

        public int Kolicina { get; set; }
        [Display(Name = "Proizvod")]
        public int ProizvodID { get; set; }
        public double CijenaPoKomadu { get; set; }
        [Display(Name = "Popust u %")]
        public double PopustUPostocima { get; set; }
        public double UkupnaCijena { get; set; }
    }
}