using System.Web.Mvc;
using Microsoft.Practices.Unity;
using PlatformSite.App_Data;

namespace PlatformSite.Controllers
{
    public class BaseController : Controller
    {
        [Dependency]
        public SessionContext SessionContext { get; set; }

        [Dependency]
        public CookieContext CookieContext { get; set; }
    }
}