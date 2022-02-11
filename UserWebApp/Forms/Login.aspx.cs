using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserWebApp.Models.Repo;

namespace UserWebApp.Forms
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly IRepo repo = RepoFactory.GetRepo();
        protected void Page_Load(object sender, EventArgs e)
        {
            AddCustomValidator();
            tbUsername.Focus();
        }

        private void AddCustomValidator()
        {
            var wrongInput = new CustomValidator();
            wrongInput.ErrorMessage = "Krivi username ili lozinka";
            Page.Validators.Add(wrongInput);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (repo.ProvjeriKorisnika(tbUsername.Text, tbPassword.Text))
            {
                HttpCookie cookie = new HttpCookie("username");
                cookie.Expires = DateTime.Now.AddMinutes(30);
                cookie["username"] = tbUsername.Text;
                Response.Cookies.Add(cookie);
                Response.Redirect("MainPage.aspx");
            }
            else
            {
                Page.Validators[Validators.Count - 1].IsValid = false;
            }
        }
    }
}