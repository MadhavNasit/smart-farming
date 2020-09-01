using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace SmartFarming.Models
{
    public class UserAuthorization : ActionFilterAttribute
    {
        public string Roles { get; set; } //rolename
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!string.IsNullOrEmpty(Roles))
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    string redirectOnSuccess = filterContext.HttpContext.Request.RawUrl;
                    string redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);
                    string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;
                    //filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Account", action = "LogOn" }));
                    filterContext.HttpContext.Response.Redirect(loginUrl, true);
                }
                else
                {
                    //whether has a role
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string roles = ticket.UserData;
                    bool isAuthorized = false;
                    if (this.Roles.IndexOf(roles) > -1)
                        isAuthorized = true;
                    else
                        isAuthorized = false;

                    //bool isAuthorized = filterContext.HttpContext.User.IsInRole(this.RoleToCheckFor);  //why always return false?

                    if (!isAuthorized)
                        filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Shared", action = "UserAuthorizedError" }));
                    //throw new UnauthorizedAccessException("no permission");
                }
            }
            else
            {
                throw new InvalidOperationException("non rolename");
            }
        }
    }

}
