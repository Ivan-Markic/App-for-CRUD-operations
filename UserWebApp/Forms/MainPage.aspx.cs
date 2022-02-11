using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserWebApp.Models;
using UserWebApp.Models.Repo;

namespace UserWebApp.Forms
{
    public partial class MainPage : System.Web.UI.Page
    {
        IRepo repo = RepoFactory.GetRepo();
        protected void Page_Load(object sender, EventArgs e)
        {
            AddCustomValidator();
            if (Request.Cookies["username"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            else if (!IsPostBack)
            {


                HttpCookie cookie = new HttpCookie("lastSorted");
                cookie.Expires = DateTime.Now.AddMinutes(60);
                cookie.Value = 0.ToString();
                Response.Cookies.Add(cookie);
                gvDataBind(int.Parse((Request.Cookies["lastSorted"].Value)), repo.DohvatiKupce().ToList());
            }
        }

        private void gvDataBind(int sortColumn, List<Customer> data)
        {
            if (sortColumn == 1)
            {

                data.Sort((c, o) => c.Ime.CompareTo(o.Ime));

            }
            else if (sortColumn == 2)
            {
                data.Sort((c, o) => c.Prezime.CompareTo(o.Prezime));
            }

            gvKupci.DataSource = data;
            gvKupci.DataBind();
            Response.Cookies["lastSorted"].Value = sortColumn.ToString();
        }

        protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            List<Customer> data = getFilteredData();

            gvKupci.SelectedIndex = -1;
            gvKupci.EditIndex = e.NewEditIndex;

            gvDataBind(int.Parse((Request.Cookies["lastSorted"].Value)), data);
            GridViewRow selectedRow = gvKupci.SelectedRow;
        }

        private List<Customer> getFilteredData()
        {
            List<Customer> data = repo.DohvatiKupce().ToList();
            City city = getFilteredCity();
            if (city != null)
            {
                data = data.Where(c => c.Grad.IDGrad == city.IDGrad).ToList();
            }
            return data;
        }

        protected void ddlDrzave_DataBinding(object sender, EventArgs e)
        {

            DropDownList list = sender as DropDownList;
            IEnumerable<Country> drzave = repo.DohvatiDrzave();
            foreach (Country country in drzave)
            {
                list.Items.Add(country.Naziv);
            }
        }

        protected void gvKupci_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            List<Customer> data = getFilteredData();
            gvKupci.EditIndex = -1;
            gvDataBind(int.Parse((Request.Cookies["lastSorted"].Value)), data);
        }

        protected void gvKupci_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = gvKupci.Rows[e.RowIndex];
            var kupacId = gvKupci.DataKeys[e.RowIndex].Value;

            var ime = gvr.Cells[0].Controls.OfType<TextBox>().First().Text;
            var prezime = gvr.Cells[1].Controls.OfType<TextBox>().First().Text;
            var email = gvr.Cells[2].Controls.OfType<TextBox>().First().Text;
            var telefon = gvr.Cells[3].Controls.OfType<TextBox>().First().Text;
            var grad = gvr.Cells[4].Controls.OfType<TextBox>().First().Text;
            var drzava = gvr.Cells[5].Controls.OfType<DropDownList>().First().Text;

            if (repo.DohvatiGrad(grad) == null)
            {
                repo.DodajGrad(grad, drzava);
            }

            Customer kupac = new Customer
            {
                IDKupac = int.Parse(kupacId.ToString()),
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Telefon = telefon,
                Grad = repo.DohvatiGrad(grad)
            };

            repo.AzurirajKupca(kupac);

            List<Customer> data = getFilteredData();

            gvKupci.EditIndex = -1;
            gvDataBind(int.Parse((Request.Cookies["lastSorted"].Value)), data);
        }

        protected void gvKupci_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<Customer> data = getFilteredData();
            gvKupci.PageIndex = e.NewPageIndex;
            gvDataBind(int.Parse((Request.Cookies["lastSorted"].Value)), data);
        }

        protected void gvKupci_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<Customer> data = getFilteredData();

            if (e.SortExpression == "Ime")
            {
                gvDataBind(1, data);
            }
            else if (e.SortExpression == "Prezime")
            {
                gvDataBind(2, data);
            }
        }

        protected void btnConfirmNumber_Click(object sender, EventArgs e)
        {
            List<Customer> data = getFilteredData();

            gvKupci.PageSize = int.Parse(tbNumberOfCustomers.Text);
            gvDataBind(int.Parse((Request.Cookies["lastSorted"].Value)), data);
        }

        private void AddCustomValidator()
        {
            var wrongInput = new CustomValidator();
            wrongInput.ValidationGroup = "CitiesValidation";
            wrongInput.ErrorMessage = "Upisani grad ne postoji";
            Page.Validators.Add(wrongInput);
            var per = Page.Validators.Count;
        }

        protected void btnSearchCities_Click(object sender, EventArgs e)
        {
            City city = getFilteredCity();
            if (city == null)
            {
                var pat = Page.Validators;
                Page.Validators[Page.Validators.Count - 1].IsValid = false;
                return;
            }
            else
            {
                List<Customer> data = repo.DohvatiKupce().ToList();
                data = data.Where(c => c.Grad.IDGrad == city.IDGrad).ToList();
                gvDataBind(int.Parse(Request.Cookies["lastSorted"].Value), data);
            }
        }

        private City getFilteredCity()
        {
            List<City> cities = GetCities();
            City city = cities.Find(c => c.Naziv.ToLower() == tbCity.Text.ToLower());
            return city;
        }

        private List<City> GetCities()
        {
            List<City> cities = repo.DohvatiGradove(1).ToList();
            cities.AddRange(repo.DohvatiGradove(2));
            cities.AddRange(repo.DohvatiGradove(3));
            return cities;
        }

        protected void gvKupci_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Staviti u metodu jer se ponavlja 2 puta, a ima puno koda
            gvKupci.EditIndex = -1;

            List<Customer> data = getFilteredData();

            gvDataBind(int.Parse((Request.Cookies["lastSorted"].Value)), data);

            if (Application["lastSelectedIndex"] != null)
            {
                gvKupci.Rows[int.Parse(Application["lastSelectedIndex"].ToString())].ForeColor = Color.White;
            }

            GridViewRow gvr = gvKupci.Rows[e.NewSelectedIndex];
            gvr.ForeColor = Color.Yellow;

            var kupacId = gvKupci.DataKeys[e.NewSelectedIndex].Value;

            var ime = gvr.Cells[0].Controls.OfType<Label>().First().Text;
            var prezime = gvr.Cells[1].Controls.OfType<Label>().First().Text;
            var email = gvr.Cells[2].Controls.OfType<Label>().First().Text;
            var telefon = gvr.Cells[3].Controls.OfType<Label>().First().Text;
            var grad = gvr.Cells[4].Controls.OfType<Label>().First().Text;
            var drzava = gvr.Cells[5].Controls.OfType<Label>().First().Text;

            Session["kupacId"] = kupacId.ToString();
            Session["ime"] = ime;
            Session["prezime"] = prezime;
            Session["email"] = email;
            Session["telefon"] = telefon;
            Session["grad"] = grad;
            Session["drzava"] = drzava;
            Session["lastSelectedIndex"] = kupacId;

            Response.Redirect("~/bill");
        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            tbCity.Text = "";
            gvDataBind(int.Parse(Request.Cookies["lastSorted"].Value), repo.DohvatiKupce().ToList());
        }

        protected void btnProizvodi_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/sviproizvodi");
        }

        protected void btnKategorije_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/prikazikategorija");
        }

        protected void btnPotkategorije_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/prikazipotkategorija");
        }
    }
}