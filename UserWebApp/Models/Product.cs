using System.ComponentModel.DataAnnotations;

namespace UserWebApp.Models
{
    public class Product
    {
        [Display(Name = "Proizvod")]
        public int IDProizvod { get; set; }
        public string Naziv { get; set; }
        public string BrojProizvoda { get; set; }
        public string Boja { get; set; }
        public int MinimalnaKolicinaNaSkladistu { get; set; }
        public double CijenaBezPDV { get; set; }
        [Display(Name = "Potkategorija")]
        public int PotkategorijaID { get; set; }
    }
}