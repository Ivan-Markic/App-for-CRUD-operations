using System.ComponentModel.DataAnnotations;

namespace UserWebApp.Models
{
    public class SubCategory
    {

        public int IDPotkategorija { get; set; }
        [Required(ErrorMessage = "Naziv potkategorije je obavezan")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Kategorija je obavezna")]
        public int KategorijaID { get; set; }

    }
}