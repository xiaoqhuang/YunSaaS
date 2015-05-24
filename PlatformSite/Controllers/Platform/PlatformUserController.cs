using System.Web.Mvc;
using Data.Model.Platform;
using Helper;
using Microsoft.Practices.Unity;
using Service.Platform;

namespace PlatformSite.Controllers.Platform
{
    public class PlatformUserController : PlatformBaseController
    {
        [Dependency]
        internal PlatformUserService UserService { private get; set; }

        public ActionResult Login()
        {
            if (UserService.LoadByUserName("admin") == null)
            {
                PlatformUser platformUser = new PlatformUser
                {
                    UserName = "admin",
                    EncryptedPassword = EncryptHelper.MD5Hash("admin"),
                    UserType = UserType.Admin
                };
                UserService.AddUser(platformUser);
            }
            return View();
        }

        public ActionResult UserLogin(string userName, string password, bool? remember)
        {
            var platformUser = UserService.Login(userName, password);
            if (platformUser == null)
            {
                return Json(new {error = "用户名或密码错误"});
            }
            SessionContext.PlatformUser = platformUser;
            if (remember.GetValueOrDefault())
            {
                CookieContext.UserName = platformUser.UserName;
                CookieContext.EncryptedPassword = platformUser.EncryptedPassword;
            }
            return Json(new {redirect = "main"});
        }

        public ActionResult Registry(RegistryType? registryType)
        {
//            switch (registryType)
//            {
//                case RegistryType.贸易商:
//                    return View("TradeRegistry");
//                case RegistryType.客户:
//                    return View("ClientRegistry");
//                case RegistryType.供应商:
//                    return View("SupplierRegistry");
//            }
            return View();
        }
    }

    public enum RegistryType
    {
        贸易商,
        客户,
        供应商
    }
}