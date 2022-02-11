using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserWebApp.Models;
using UserWebApp.Models.Repo;

namespace UserWebApp.Controllers
{
    public class SubCategoryController : Controller
    {
        IRepo repo = RepoFactory.GetRepo();
        // GET: SubCategory
        [Route("~/prikazipotkategorija")]
        public ActionResult DohvatiSvePotKategorije()
        {
            if (Request.Cookies["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            var potkategorije = repo.DohvatiPotkategorije();
            return View(potkategorije);
        }

        [HttpGet]
        [Route("~/kreiranjepotkategorije")]
        public ActionResult KreiranjePotkategorije()
        {
            if (Request.Cookies["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ViewBag.Kategorije = repo.DohvatiKategorije();
            return View();
        }

        [HttpPost]
        [Route("~/kreiranjepotkategorije")]
        public ActionResult KreiranjePotkategorije(SubCategory potkategorija)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Kategorije = repo.DohvatiKategorije();
                return View(potkategorija);
            }
            repo.DodajPotkategoriju(potkategorija);
            return RedirectToAction("DohvatiSvePotKategorije");
        }

        public ActionResult BrisanjePotkategorije(int potkategorijaID)
        {
            repo.ObrisiPotkategoriju(potkategorijaID);
            return RedirectToAction("DohvatiSvePotKategorije");
        }

        [HttpGet]
        public ActionResult UrediPotkategoriju(int potkategorijaID)
        {
            ViewBag.Kategorije = repo.DohvatiKategorije();
            var potkategorija = repo.DohvatiPotkategoriju(potkategorijaID);
            return View(potkategorija);
        }

        [HttpPost]
        public ActionResult UrediPotkategoriju(SubCategory potkategorija)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Kategorije = repo.DohvatiKategorije();
                return View(potkategorija);
            }
            repo.AzurirajPotkategoriju(potkategorija);
            return RedirectToAction("DohvatiSvePotKategorije");
        }

        public ActionResult DohvatiKategoriju(int kategorijaID)
        {
            var kategorija = repo.DohvatiKategoriju(kategorijaID);
            return PartialView(viewName:"_Kategorija", model:kategorija);
        }

    }
}