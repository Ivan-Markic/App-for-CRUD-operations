using System.ComponentModel.DataAnnotations;

namespace UserWebApp.Models
{
    public class Category
    {
        public int IDKategorija { get; set; }

        [Required(ErrorMessage = "Naziv kategorije je obavezan")]
        public string Naziv { get; set; }
    }
}