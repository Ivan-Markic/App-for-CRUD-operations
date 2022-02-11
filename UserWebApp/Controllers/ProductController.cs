using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserWebApp.Models;
using UserWebApp.Models.Repo;

namespace UserWebApp.Controllers
{
    public class ProductController : Controller
    {
        IRepo repo = RepoFactory.GetRepo();
        // GET: Product
        [Route("~/sviproizvodi")]
        public ActionResult SviProizvodi()
        {
            if (Request.Cookies["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            return View("Product");
        }

        [HttpGet]
        public ActionResult PrikazUredivanjaProizvoda(int id)
        {
            var proizvod = repo.DohvatiProizvod(id);
            return View(proizvod);
        }

        [Route("~/kreiranjeproizvoda")]
        public ActionResult KreiranjeProizvoda()
        {
            if (Request.Cookies["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            ViewBag.PotKategorije = repo.DohvatiPotkategorije();
            return View();
        }

        [HttpPost]
        [Route("~/kreiranjeproizvoda")]
        public ActionResult KreiranjeProizvoda(Product proizvod)
        {
            proizvod = proizvod;
            if (!ModelState.IsValid)
            {
                ViewBag.PotKategorije = repo.DohvatiPotkategorije();
                return View(proizvod);
            }
            repo.DodajProizvod(proizvod);
            return RedirectToAction("SviProizvodi");
        }


    }
}