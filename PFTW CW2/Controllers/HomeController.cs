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

        public ActionResult Logout()
        {
            HttpCookie sessionID = new HttpCookie("sessionID");
            sessionID.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(sessionID);

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
            return View(db.Causes.ToList());
        }

        public ActionResult ViewCauses()
        {
            ViewBag.Message = "View Causes";
            return View(db.Causes.ToList());
        }

        public ActionResult ViewCause(int causeID)
        {
            var causeFromDB = db.Causes.SingleOrDefault(cause => cause.id == causeID);

            if (causeFromDB.id == causeID)
            {
                return View(causeFromDB);
            }

            if (causeFromDB == null)
            {
                ViewBag.Message = "Cause was not found.";
                return View("Error");
            }
            else
            {
                ViewBag.Message = "Cause was not found.";
                return View("Error");
            }
        }

        public ActionResult CreateCause()
        {
            ViewBag.Message = "Create your cause below!";
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
                    isActive = true,
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
                
                return View("Index");
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

                return View("Index");
            } else
            {
                ViewBag.Message = "An unknown error occured.";
                return View("Error");
            }
            
        }

        [HttpPost]
        public ActionResult AddCause(String title, String description, String imageURL)
        {
            var causeFromDB = db.Causes.SingleOrDefault(cause => cause.title == title);

            if (causeFromDB == null)
            {
                var newID = db.Causes.Count() + 1;
                Cause newCause = new Cause
                {
                    id = newID,
                    title = title,
                    description = description,
                    imageURL = imageURL,
                    isActive = true,
                };

                db.Causes.Add(newCause);
                db.SaveChanges();

                var userIDAsString = Request.Cookies["sessionID"].Value;

                if (userIDAsString == "admin")
                {
                    var admin = db.Users.SingleOrDefault(u => u.isAdmin == true);
                    newCause.owner = admin;
                    admin.UserCauses.Add(newCause);
                }
                else
                {
                    var userID = Convert.ToInt32(userIDAsString);
                    var user = db.Users.SingleOrDefault(u => u.id == userID);
                    newCause.owner = user;
                    user.UserCauses.Add(newCause);
                }

                ViewBag.Message = "Cause successfully added!";
                return View("CreateCause");
            }

            if (causeFromDB.title == title)
            {
                ViewBag.Message = "A cause with this title already exists.";
                return View("Error");
            }

            if (causeFromDB.description == description)
            {
                ViewBag.Message = "A cause with this description already exists.";
                return View("Error");
            }
            else
            {
                ViewBag.Message = "An unknown error occured";
                return View("Error");
            }
        }

        public ActionResult removeCause(int causeID)
        {
            var causeFromDB = db.Causes.SingleOrDefault(cause => cause.id == causeID);

            if (causeFromDB != null)
            {
                db.Causes.Remove(causeFromDB);
                db.SaveChanges();
                return View("AdminPage", db.Causes.ToList());
            }
            else
            {
                ViewBag.Message = "An unknown error occured";
                return View("Error");
            }
        }

        public ActionResult AddName(int causeID)
        {
            var causeFromDB = db.Causes.SingleOrDefault(cause => cause.id == causeID);

            var userIDAsString = Request.Cookies["sessionID"].Value;

            if (causeFromDB.id == causeID && userIDAsString == "admin")
            {
                var signature = db.Users.SingleOrDefault(user => user.isAdmin == true);

                var userToCheck = causeFromDB.signatureList.SingleOrDefault(user => user.id == signature.id);

                if(userToCheck == signature)
                {
                    ViewBag.Message = "You have already signed this cause";
                    return View("Error");
                }
                else
                {
                    causeFromDB.signatureList.Add(signature);
                    causeFromDB.signatureCount++;
                    db.SaveChanges();
                }

            } else
            {
                var userID = Convert.ToInt32(userIDAsString);
                var signature = db.Users.SingleOrDefault(user => user.id == userID);

                var userToCheck = causeFromDB.signatureList.SingleOrDefault(user => user.id == signature.id);

                if (userToCheck == signature)
                {
                    ViewBag.Message = "You have already signed this cause";
                    return View("Error");
                }
                else
                {
                    causeFromDB.signatureList.Add(signature);
                    causeFromDB.signatureCount++;
                    db.SaveChanges();
                }
            }

            return View("ViewCause", causeFromDB);
        }

        public ActionResult RemoveName(int causeID)
        {
            var causeFromDB = db.Causes.SingleOrDefault(cause => cause.id == causeID);

            var userIDAsString = Request.Cookies["sessionID"].Value;

            if (causeFromDB.id == causeID && userIDAsString == "admin")
            {
                var signature = db.Users.SingleOrDefault(user => user.isAdmin == true);

                var userToCheck = causeFromDB.signatureList.SingleOrDefault(user => user.id == signature.id);

                if (userToCheck == null)
                {
                    ViewBag.Message = "You have not signed this cause";
                    return View("Error");
                }
                else
                {
                    causeFromDB.signatureList.Remove(signature);
                    causeFromDB.signatureCount--;
                    db.SaveChanges();
                }

            }
            else
            {
                var userID = Convert.ToInt32(userIDAsString);
                var signature = db.Users.SingleOrDefault(user => user.id == userID);

                var userToCheck = causeFromDB.signatureList.SingleOrDefault(user => user.id == signature.id);

                if (userToCheck == null)
                {
                    ViewBag.Message = "You have not signed this cause";
                    return View("Error");
                }
                else
                {
                    causeFromDB.signatureList.Remove(signature);
                    causeFromDB.signatureCount--;
                    db.SaveChanges();
                }
            }

            return View("ViewCause", causeFromDB);
        }

    }
    }