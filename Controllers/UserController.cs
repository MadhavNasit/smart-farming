using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SmartFarming.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(User userModel, string returnUrl)
        {
            
            using (SmartFarmingEntities db = new SmartFarmingEntities())
            {
                var userDetails = db.Users.Where(x => x.Email == userModel.Email && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    ModelState.AddModelError("", "Wrong User Name or Password");
                    return View("LogIn", userModel);
                }
                else
                {
                    Session["UserId"] = userDetails.id;
                    Session["Email"] = userDetails.Email;

                    if (userDetails.Role.Title == "Admin")
                    {
                        Session["Roles"] = userDetails.Role.Title;
                        FormsAuthentication.SetAuthCookie(userModel.Email,false);
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("ManageDealer", "Admin");
                        }
                    }
                    else if (userDetails.Role.Title == "Dealer")
                    {
                        Session["Roles"] = userDetails.Role.Title;
                        FormsAuthentication.SetAuthCookie(userModel.Email, false);
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("ManageSeeds", "Dealer");
                        }
                    }
                    else if (userDetails.Role.Title == "Tractor")
                    {
                        Session["Roles"] = userDetails.Role.Title;
                        FormsAuthentication.SetAuthCookie(userModel.Email, false);
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("ManageTractors", "Dealer");
                        }
                    }
                    else
                    {
                        return View("LogIn", userModel);
                    }
                }
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "User");
        }
    }
}