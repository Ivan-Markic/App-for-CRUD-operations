using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebApp.Models.Repo
{
    public class RepoFactory
    {
        public static IRepo GetRepo() => new SQLRepo();
    }
}