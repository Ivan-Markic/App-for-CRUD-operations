using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserWebApp.Models.Repo;

namespace UserWebApp.Forms
{
    public partial class Register : System.Web.UI.Page
    {
        private IRepo repo = RepoFactory.GetRepo();
        protected void Page_Load(object sender, EventArgs e)
        {
            tbUsername.Focus();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (repo.PostojiLiKorisnik(tbUsername.Text))
            {
                CustomValidator.IsValid = false;
            }
            else
            {
                repo.DodajKorisnika(tbUsername.Text, tbPassword.Text);
                Response.Redirect("Login.aspx");
            }
        }
    }
}