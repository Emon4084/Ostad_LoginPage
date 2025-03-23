using Login_FogetPassword.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login_FogetPassword.Controllers.LogIn
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            BaseMember member = new BaseMember();
            List<BaseMember> members = member.GetLoginMembers(username, password);
            bool success = false;

            foreach (BaseMember baseMember in members)
            {
                if (baseMember.UserName == username && baseMember.Password == password)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }

            if (success)
            {
                Session["UserName"] = username;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Invalid Username or Password!";
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewPassword(string userName, string newPassword, string confirmPassword)
        {
            BaseMember member = new BaseMember();
            List<BaseMember> members = member.GetMembers(userName);

            if (members.Count == 0)
            {
                TempData["Error"] = "Invalid Username!";
                return RedirectToAction("ForgetPassword");
            }

            if (newPassword != confirmPassword)
            {
                TempData["Error"] = "Passwords do not match!";
                return RedirectToAction("ForgetPassword");
            }

            bool passwordUpdated = member.UpdateNewPassword(userName, newPassword);

            if (passwordUpdated)
            {
                TempData["Success"] = "Password updated successfully!";
                return RedirectToAction("Login");
            }

            TempData["Error"] = "Error updating password.";
            return RedirectToAction("ForgetPassword");
        }

        
    }
}