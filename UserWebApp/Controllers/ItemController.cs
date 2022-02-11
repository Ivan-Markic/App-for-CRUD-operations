using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserWebApp.Models;
using UserWebApp.Models.Repo;

namespace UserWebApp.Controllers
{
    public class ItemController : Controller
    {
        IRepo repo = RepoFactory.GetRepo();
        // GET: Item
        public ActionResult ShowItems(int racunID)
        {
            if (Request.Cookies["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            var Stavke = repo.DohvatiStavke(racunID);
            return View(Stavke);
        }
    }
}