using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Login_FogetPassword.Models;

namespace Login_FogetPassword.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View("Registration");
        }

        [HttpPost]
        public ActionResult Register(Registration model)
        {
            if (ModelState.IsValid)
            {
                bool isSaved = model.SaveRegistration();
                if (isSaved)
                {
                    TempData["SuccessMessage"] = "Registration successful!";
                    return RedirectToAction("RegisterUser");
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurred while saving the data.";
                }
            }

            return View("Registration", model);

            
        }


        public ActionResult RegisterUser()
        {
            List<Registration> users = Registration.GetAllUsers();
            return View(users);
        }



    }
}