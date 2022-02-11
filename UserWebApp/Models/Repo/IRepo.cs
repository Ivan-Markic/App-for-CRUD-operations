using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace UserWebApp.Models.Repo
{
    public interface IRepo
    {
        IEnumerable<Country> DohvatiDrzave();
        IEnumerable<City> DohvatiGradove(int drzavaID);

        City DohvatiGrad(string naziv);
        bool ProvjeriKorisnika(string ime, string lozinka);
        IEnumerable<Item> DohvatiStavke(int racunID);
        IEnumerable<Bill> DohvatiRacune(int korisnikID);
        bool PostojiLiKorisnik(string ime);
        int DodajKorisnika(string ime, string lozinka);

        DataTable DohvatiTablicuKupaca();

        IEnumerable<Customer> DohvatiKupce();
        void AzurirajKupca(Customer data);
        void DodajGrad(string grad, string drzava);
        IEnumerable<Product> DohvatiProizvode();
        Product DohvatiProizvod(int ProizvodID);
        void AzurirajProizvod(Product product);
        void DodajProizvod(Product product);
        void ObrisiProizvod(int proizvodID);

        IEnumerable<SubCategory> DohvatiPotkategorije();
        SubCategory DohvatiPotkategoriju(int potkategorijeID);
        void AzurirajPotkategoriju(SubCategory potkategorija);
        void DodajPotkategoriju(SubCategory potkategorija);
        void ObrisiPotkategoriju(int potkategorijaID);
        IEnumerable<Category> DohvatiKategorije();
        Category DohvatiKategoriju(int kategorijeID);
        void AzurirajKategoriju(Category kategorija);
        void DodajKategoriju(Category kategorija);
        void ObrisiKategoriju(int kategorijaID);
    }
}