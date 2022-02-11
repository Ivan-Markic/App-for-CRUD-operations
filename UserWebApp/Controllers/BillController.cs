using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserWebApp.Models;
using UserWebApp.Models.Repo;

namespace UserWebApp.Controllers
{
    public class BillController : Controller
    {
        IRepo repo = RepoFactory.GetRepo();

        // GET: Bill
        [Route("~/bill")]
        public ActionResult ShowBills()
        {
            if (Request.Cookies["username"] == null || Session["kupacId"] == null)
            {
                Response.Redirect("/Forms/Login.aspx");
            }

            IEnumerable<Bill> bills = repo.DohvatiRacune(int.Parse(Session["kupacId"].ToString()));

            if (bills.Count() == 0)
            {
                return View(viewName: "ShowBills", bills);
            }

            return View(bills);
        }
    }
}