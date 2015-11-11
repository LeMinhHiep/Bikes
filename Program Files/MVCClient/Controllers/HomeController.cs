using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using MVCClient.Models;

namespace MVCClient.Controllers
{
    public class HomeController : CoreController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserGuide()
        {
            if (User.Identity.IsAuthenticated)
            {
                string aspUserID = User.Identity.GetUserId();

                var Db = new ApplicationDbContext();

                var userID = Db.Users.Where(w => w.Id == aspUserID).FirstOrDefault().UserID;
            }

            return View();
        }
    }
}