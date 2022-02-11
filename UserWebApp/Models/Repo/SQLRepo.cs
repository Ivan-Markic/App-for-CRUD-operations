using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace UserWebApp.Models.Repo
{
    public class SQLRepo : IRepo
    {
        private readonly string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

        public int DodajKorisnika(string ime, string lozinka)
        {
            int Id = 0;
            return SqlHelper.ExecuteNonQuery(cs, "DodajKorisnika", ime, lozinka, Id);
        }

        public IEnumerable<City> DohvatiGradove(int drzavaId)
        {
            var tblGradovi = SqlHelper.ExecuteDataset(cs, "DohvatiGradove", drzavaId).Tables[0];

            foreach (DataRow row in tblGradovi.Rows)
            {
                yield return new City
                {
                    IDGrad = (int)row["IDGrad"],
                    Naziv = row["Naziv"].ToString(),
                    Drzava = new Country
                    {
                        IDDrzava = (int)row["IDDrzava"],
                        Naziv = row[3].ToString()
                    }
                };
            }
        }

        public IEnumerable<Customer> DohvatiKupce()
        {
            var tblKupci = SqlHelper.ExecuteDataset(cs, "DohvatiKupce").Tables[0];
            foreach (DataRow row in tblKupci.Rows)
            {
                yield return new Customer
                {
                    IDKupac = (int)row["IDKupac"],
                    Ime = row["Ime"].ToString(),
                    Prezime = row["Prezime"].ToString(),
                    Email = row["Email"].ToString(),
                    Telefon = row["Telefon"].ToString(),
                    Grad = new City
                    {
                        IDGrad = (int)row["IDGrad"],
                        Naziv = row["Naziv"].ToString(),
                        Drzava = new Country
                        {
                            IDDrzava = (int)row["IDDrzava"],
                            Naziv = row[8].ToString()
                        }
                    }
                };
            }
        }

        public DataTable DohvatiTablicuKupaca()
        {
            return SqlHelper.ExecuteDataset(cs, "DohvatiKupce").Tables[0];
        }

        public IEnumerable<Country> DohvatiDrzave()
        {
            var tblCountries = SqlHelper.ExecuteDataset(cs, "GetDrzave").Tables[0];
            foreach (DataRow row in tblCountries.Rows)
            {
                yield return new Country
                {
                    IDDrzava = (int)row["IDDrzava"],
                    Naziv = row["Naziv"].ToString()
                };
            }
        }

        public bool PostojiLiKorisnik(string ime)
        {
            var tblUser = SqlHelper.ExecuteDataset(cs, "PostojiLiKorisnik", ime).Tables[0];

            return tblUser.Rows.Count == 0 ? false : true;
        }

        public bool ProvjeriKorisnika(string ime, string lozinka)
        {
            var tblUser = SqlHelper.ExecuteDataset(cs, "ProvjeriKorisnika", ime, lozinka).Tables[0];

            return tblUser.Rows.Count == 0 ? false : true;
        }

        public City DohvatiGrad(string naziv)
        {
            var tblGrad = SqlHelper.ExecuteDataset(cs, "DohvatiGrad", naziv).Tables[0];
            if (tblGrad.Rows.Count == 0)
            {
                return null;
            }
            var row = tblGrad.Rows[0];
            return new City
            {
                IDGrad = (int)row["IDGrad"],
                Naziv = row["Naziv"].ToString(),
                Drzava = new Country
                {
                    IDDrzava = (int)row["IDDrzava"],
                    Naziv = row[3].ToString()
                }
            };
        }

        public void AzurirajKupca(Customer data)
        {
            SqlHelper.ExecuteNonQuery(cs, "AzurirajKupca", data.IDKupac, data.Ime, data.Prezime, data.Email,
                data.Telefon, data.Grad.IDGrad);
        }

        public void DodajGrad(string nazivGrada, string nazivDrzava)
        {
            SqlHelper.ExecuteNonQuery(cs, "DodajGrad", nazivGrada, nazivDrzava);
        }

        public IEnumerable<Bill> DohvatiRacune(int korisnikID)
        {
            var tblRacuni = SqlHelper.ExecuteDataset(cs, "DohvatiRacune", korisnikID).Tables[0];
            foreach (DataRow row in tblRacuni.Rows)
            {
                yield return new Bill
                {
                    IDRacun = (int)row["IDRacun"],
                    DatumIzdavanja = DateTime.Parse(row["DatumIzdavanja"].ToString()),
                    BrojRacuna = row["BrojRacuna"].ToString(),
                    Komercijalist = $"{row["KomercijalistIme"]} {row["KomercijalistPrezime"]}",
                    Kupac = new Customer
                    {
                        IDKupac = (int)row["IDKupac"],
                        Ime = row["Ime"].ToString(),
                        Prezime = row["Prezime"].ToString(),
                    },
                    KreditnaKartica = row["Tip"].ToString()
                };
            }
        }

        public IEnumerable<Item> DohvatiStavke(int racunID)
        {
            var tblStavke = SqlHelper.ExecuteDataset(cs, "DohvatiStavke", racunID).Tables[0];
            foreach (DataRow row in tblStavke.Rows)
            {
                yield return new Item
                {
                    IDStavka = (int)row["IDStavka"],
                    Racun = new Bill
                    {
                        IDRacun = (int)row["IDRacun"],
                        BrojRacuna = row["BrojRacuna"].ToString()
                    },
                    Kolicina = int.Parse(row["Kolicina"].ToString()),
                    ProizvodID = (int)row["IDProizvod"],
                    CijenaPoKomadu = double.Parse(row["CijenaPoKomadu"].ToString()),
                    PopustUPostocima = double.Parse(row["PopustUPostocima"].ToString()) * 100,
                    UkupnaCijena = double.Parse(row["UkupnaCijena"].ToString())
                };
            }
        }

        public IEnumerable<Product> DohvatiProizvode()
        {
            var tblProizvodi = SqlHelper.ExecuteDataset(cs, "DohvatiProizvode").Tables[0];
            foreach (DataRow row in tblProizvodi.Rows)
            {
                yield return new Product
                {
                    IDProizvod = (int)row["IDProizvod"],
                    Naziv = row["Naziv"].ToString(),
                    BrojProizvoda = row["BrojProizvoda"].ToString(),
                    Boja = row["Boja"].ToString() == string.Empty ? "nema podatka" : row["Boja"].ToString(),
                    MinimalnaKolicinaNaSkladistu = (int)row["IDProizvod"],
                    CijenaBezPDV = double.Parse(row["IDProizvod"].ToString()),
                    PotkategorijaID = int.Parse(row["PotkategorijaID"].ToString() == string.Empty ? "0" : row["PotkategorijaID"].ToString())
                };
            }
        }

        public Product DohvatiProizvod(int proizvodID)
        {
            var tblProizvod = SqlHelper.ExecuteDataset(cs, "DohvatiProizvod", proizvodID).Tables[0];
            if (tblProizvod.Rows.Count == 0)
            {
                return null;
            }
            var row = tblProizvod.Rows[0];
            return new Product
            {
                IDProizvod = proizvodID,
                Naziv = row["Naziv"].ToString(),
                BrojProizvoda = row["BrojProizvoda"].ToString(),
                Boja = row["Boja"].ToString() == string.Empty ? "nema podatka" : row["Boja"].ToString(),
                MinimalnaKolicinaNaSkladistu = (int)row["IDProizvod"],
                CijenaBezPDV = double.Parse(row["IDProizvod"].ToString()),
                PotkategorijaID = int.Parse(row["PotkategorijaID"].ToString() == string.Empty ? "0" : row["PotkategorijaID"].ToString())
            };
        }

        public void AzurirajProizvod(Product proizvod)
        {
            SqlHelper.ExecuteNonQuery(cs, "AzurirajProizvod", proizvod.IDProizvod, proizvod.Naziv,
                proizvod.BrojProizvoda, proizvod.Boja,
                proizvod.MinimalnaKolicinaNaSkladistu, proizvod.CijenaBezPDV,
                proizvod.PotkategorijaID);
        }

        public void DodajProizvod(Product proizvod)
        {
            SqlHelper.ExecuteNonQuery(cs, "DodajProizvod", proizvod.Naziv,
                proizvod.BrojProizvoda, proizvod.Boja,
                proizvod.MinimalnaKolicinaNaSkladistu, proizvod.CijenaBezPDV,
                proizvod.PotkategorijaID);
        }

        public void ObrisiProizvod(int proizvodID)
        {
            SqlHelper.ExecuteNonQuery(cs, "ObrisiProizvod", proizvodID);
        }

        public IEnumerable<SubCategory> DohvatiPotkategorije()
        {
            var tblPotkategorije = SqlHelper.ExecuteDataset(cs, "DohvatiPotkategorije").Tables[0];
            foreach (DataRow row in tblPotkategorije.Rows)
            {
                yield return new SubCategory
                {
                    IDPotkategorija = int.Parse(row["IDPotkategorija"].ToString()),
                    Naziv = row["Naziv"].ToString(),
                    KategorijaID = int.Parse(row["KategorijaID"].ToString())
                };
            }
        }

        public SubCategory DohvatiPotkategoriju(int potkategorijeID)
        {
            var tblPotkategorije = SqlHelper.ExecuteDataset(cs, "DohvatiPotkategoriju", potkategorijeID).Tables[0];
            if (tblPotkategorije.Rows.Count == 0)
            {
                return null;
            }
            var row = tblPotkategorije.Rows[0];
            return new SubCategory
            {
                IDPotkategorija = int.Parse(row["IDPotkategorija"].ToString()),
                Naziv = row["Naziv"].ToString(),
                KategorijaID = int.Parse(row["KategorijaID"].ToString())
            };
        }

        public void AzurirajPotkategoriju(SubCategory potkategorija)
        {
            SqlHelper.ExecuteNonQuery(cs, "AzurirajPotkategoriju", potkategorija.IDPotkategorija, potkategorija.Naziv,
                potkategorija.KategorijaID);
        }

        public void DodajPotkategoriju(SubCategory potkategorija)
        {
            SqlHelper.ExecuteNonQuery(cs, "DodajPotkategoriju",potkategorija.KategorijaID, potkategorija.Naziv);
        }

        public void ObrisiPotkategoriju(int potkategorijaID)
        {
            SqlHelper.ExecuteNonQuery(cs, "ObrisiPotkategoriju", potkategorijaID);
        }

        public IEnumerable<Category> DohvatiKategorije()
        {
            var tblKategorije = SqlHelper.ExecuteDataset(cs, "DohvatiKategorije").Tables[0];
            foreach (DataRow row in tblKategorije.Rows)
            {
                yield return new Category
                {
                    IDKategorija = int.Parse(row["IDKategorija"].ToString()),
                    Naziv = row["Naziv"].ToString()
                };
            }
        }

        public Category DohvatiKategoriju(int kategorijeID)
        {
            var tblKategorije = SqlHelper.ExecuteDataset(cs, "DohvatiKategoriju", kategorijeID).Tables[0];
            if (tblKategorije.Rows.Count == 0)
            {
                return null;
            }
            var row = tblKategorije.Rows[0];
            return new Category
            {
                IDKategorija = int.Parse(row["IDKategorija"].ToString()),
                Naziv = row["Naziv"].ToString()
            };
        }

        public void AzurirajKategoriju(Category kategorija)
        {
            SqlHelper.ExecuteNonQuery(cs, "AzurirajKategoriju", kategorija.IDKategorija, kategorija.Naziv);
        }

        public void DodajKategoriju(Category kategorija)
        {
            SqlHelper.ExecuteNonQuery(cs, "DodajKategoriju", kategorija.Naziv);
        }

        public void ObrisiKategoriju(int kategorijaID)
        {
            SqlHelper.ExecuteNonQuery(cs, "ObrisiKategoriju", kategorijaID);
        }
    }
}