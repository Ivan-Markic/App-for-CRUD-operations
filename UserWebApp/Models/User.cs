using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebApp.Models
{
    public class User
    {
        public int IDKorisnik { get; set; }
        public string Ime { get; set; }
        public string Lozinka { get; set; }
    }
}