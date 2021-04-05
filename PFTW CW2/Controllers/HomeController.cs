using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PFTW_CW2.Models;

namespace PFTW_CW2.Controllers
{
    public class HomeController : Controller
    {
        StaticData data = StaticData.Instance;
        CauseCollectiveDB db = new CauseCollectiveDB();

        public ActionResult Index()
        {
            db.Causes.Add()
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login";
            return View();
        }
    }
}