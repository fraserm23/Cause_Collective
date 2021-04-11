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
        CauseCollectiveDB db = new CauseCollectiveDB();
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "";
            return View();
        }

        public ActionResult Registration()
        {
            ViewBag.Message = "Registration";
            return View();
        }

        public ActionResult AdminPage()
        {
            ViewBag.Message = "Admin Page";
            return View();
        }

        public ActionResult ViewCauses()
        {
            ViewBag.Message = "View Causes";
            return View();
        }

        public ActionResult Error()
        {
            ViewBag.Message = "Error";
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(String firstName, String lastName, String email, String password, bool AgreeTerms)
        {
            var userFromDB = db.Users.SingleOrDefault(user => user.email == email);

            if(userFromDB == null && AgreeTerms == true)
            {
                var newID = db.Users.Count() +1;
                User newUser = new User
                {
                    id = newID,
                    firstName = firstName,
                    lastName = lastName,
                    email = email,
                    password = password,
                    isAdmin = false,
                    isActive = true
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                ViewBag.Message = "Successful registration!";
                return View("Login");
            }

            if(userFromDB.email == email)
            {
                ViewBag.Message = "There is already an account associated with this email.";
                return View("Error");
            } else
            {
                ViewBag.Message = "An unknown error occured.";
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult CheckLogin(String email, String password, bool stayLoggedIn)
        {
            var admin = db.Users.SingleOrDefault(user => user.isAdmin == true);
                
            if(email == admin.email && password == admin.password)
            {
                Response.Cookies["sessionID"].Value = "admin";

                if (stayLoggedIn == true)
                {
                    Response.Cookies["sessionID"].Expires = DateTime.Now.AddDays(7);
                }
                
                return View("AdminPage");
            }

            var userFromDB = db.Users.SingleOrDefault(user => user.email == email);

            if(userFromDB == null)
            {
                ViewBag.Message = "Could not find the account associated with these details.";
                return View("Error");
            }

            if(email == userFromDB.email && password == userFromDB.password && userFromDB.isActive == true)
            {
                Response.Cookies["sessionID"].Value = userFromDB.id.ToString();

                if (stayLoggedIn == true)
                {
                    Response.Cookies["sessionID"].Expires = DateTime.Now.AddDays(7);
                }

                return View("ViewCauses");
            } else
            {
                ViewBag.Message = "An unknown error occured.";
                return View("Error");
            }
            
        }
    }
}