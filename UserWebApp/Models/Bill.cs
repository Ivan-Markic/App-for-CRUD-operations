using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserWebApp.Models
{
    public class Bill
    {
        public int IDRacun { get; set; }
        [Display(Name = "Datum Izdavanja")]
        public DateTime DatumIzdavanja { get; set; }
        [Display(Name = "Broj Racuna")]
        public string BrojRacuna { get; set; }
        public Customer Kupac { get; set; }
        public string Komercijalist { get; set; }
        [Display(Name = "Kreditna Kartica")]
        public string KreditnaKartica { get; set; }
    }
}