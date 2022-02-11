using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserWebApp.Models;
using UserWebApp.Models.Repo;

namespace UserWebApp.Controllers
{
    public class ProductsController : ApiController
    {
        IRepo repo = RepoFactory.GetRepo();
        [HttpGet]
        public IHttpActionResult DohvatiProizvode()
        {
            var proizvodi = repo.DohvatiProizvode();
            return Ok(proizvodi);
        }

        [HttpGet]
        public IHttpActionResult DohvatiProizvod(int id)
        {
            var proizvod = repo.DohvatiProizvod(id);
            if (proizvod == null)
            {
                return NotFound();
            }
            return Ok(proizvod);
        }

        [HttpPost]
        public IHttpActionResult DodajProizvod(Product proizvod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            repo.DodajProizvod(proizvod);
            return Ok(proizvod);
        }

        [HttpPut]
        public IHttpActionResult UrediProizvod(int id, Product proizvod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var ProizvodIzBaze = repo.DohvatiProizvod(id);

            if (ProizvodIzBaze == null)
            {
                return NotFound();
            }
            proizvod.IDProizvod = id;
            repo.AzurirajProizvod(proizvod);

            return Ok(proizvod);
        }

        [HttpDelete]
        public IHttpActionResult ObrisiProizvod(int id)
        {
            var proizvod = repo.DohvatiProizvod(id);
            if (proizvod == null)
            {
                return NotFound();
            }
            repo.ObrisiProizvod(id);
            return Ok("Uspjesno obrisano");
        }
    }
}
