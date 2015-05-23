using System.Web.Mvc;
using Data.Model.Platform;
using Microsoft.Practices.Unity;
using Service.Platform;

namespace PlatformSite.Controllers.Platform
{
    public class PlatformUserController : PlatformBaseController
    {
        [Dependency]
        public PlatformUserService UserService { private get; set; }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult UserLogin(string userName, string password)
        {
            PlatformUser platformUser = UserService.Login(userName, password);
            if (platformUser == null)
            {
                ViewBag.LoginFailed = true;
                return Login();
            }
            SessionContext.PlatformUser = platformUser;
            return Redirect("Main");
        }
    }
}