using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserWebApp.Models;
using UserWebApp.Models.Repo;

namespace UserWebApp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        IRepo repo = RepoFactory.GetRepo();
        [Route("~/prikazikategorija")]
        public ActionResult PrikazKategorija()
        {
            if (Request.Cookies["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            var kategorije = repo.DohvatiKategorije();
            return View(kategorije);
        }
        [HttpGet]
        [Route("~/kreiranjekategorije")]
        public ActionResult KreiranjeKategorije()
        {
            if (Request.Cookies["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            return View();
        }

        [HttpPost]
        [Route("~/kreiranjekategorije")]
        public ActionResult KreiranjeKategorije(Category kategorija)
        {

            if (!ModelState.IsValid)
            {
                return View(kategorija);
            }
            repo.DodajKategoriju(kategorija);
            return RedirectToAction("PrikazKategorija");
        }

        public ActionResult BrisanjeKategorije(int kategorijaID)
        {
            repo.ObrisiKategoriju(kategorijaID);
            return RedirectToAction("PrikazKategorija");
        }

        [HttpGet]
        public ActionResult UrediKategoriju(int kategorijaID)
        {
            var kategorija = repo.DohvatiKategoriju(kategorijaID);
            return View(kategorija);
        }

        [HttpPost]
        public ActionResult UrediKategoriju(Category kategorija)
        {
            if (Request.Cookies["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!ModelState.IsValid)
            {
                return View(kategorija);
            }
            repo.AzurirajKategoriju(kategorija);
            return RedirectToAction("PrikazKategorija");
        }
    }
}