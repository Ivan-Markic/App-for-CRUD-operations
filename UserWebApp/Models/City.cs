using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebApp.Models
{
    public class City
    {
        public int IDGrad { get; set; }
        public string Naziv { get; set; }
        public Country Drzava { get; set; }

        public override string ToString()
        => $"{Naziv}({Drzava.Naziv})";
    }
}