using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserWebApp.App_Start;
using UserWebApp.Models;
using UserWebApp.Models.Repo;

namespace UserWebApp.Controllers
{
    public class SubCategoriesController : ApiController
    {
        IRepo repo = RepoFactory.GetRepo();
        [HttpGet]
        public IHttpActionResult DohvatiPotkategorije()
        {
            var potkategorije = repo.DohvatiPotkategorije();
            var potkategorijeDto = AutoMapperConfig.Mapper.Map<IEnumerable<SubCategoryDto>>(potkategorije);
            return Ok(potkategorijeDto);
        }

        [HttpGet]
        public IHttpActionResult DohvatiPotkategoriju(int id)
        {
            var potkategorija = repo.DohvatiPotkategoriju(id);
            if (potkategorija == null)
            {
                return NotFound();
            }
            var potkategorijaDto = AutoMapperConfig.Mapper.Map<IEnumerable<SubCategoryDto>>(potkategorija);
            return Ok(potkategorijaDto);
        }

        [HttpPost]
        public IHttpActionResult DodajPotkategoriju(SubCategory potkategorija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            repo.DodajPotkategoriju(potkategorija);
            return Ok(potkategorija);
        }

        [HttpPut]
        public IHttpActionResult UrediPotkategoriju(int id, SubCategory potkategorija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var potkategorijaIzBaze = repo.DohvatiPotkategoriju(id);

            if (potkategorijaIzBaze == null)
            {
                return NotFound();
            }
            potkategorija.IDPotkategorija = id;
            repo.AzurirajPotkategoriju(potkategorija);

            return Ok(potkategorija);
        }

        [HttpDelete]
        public IHttpActionResult ObrisiPotkategoriju(int id)
        {
            var potkategorija = repo.DohvatiPotkategoriju(id);
            if (potkategorija == null)
            {
                return NotFound();
            }
            repo.ObrisiPotkategoriju(id);
            return Ok("Uspjesno obrisano");
        }
    }
}
