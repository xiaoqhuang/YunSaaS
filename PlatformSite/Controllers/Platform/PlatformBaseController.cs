using System.Web.Mvc;
using Microsoft.Practices.Unity;
using PlatformSite.App_Data;

namespace PlatformSite.Controllers.Platform
{
    public class PlatformBaseController : Controller
    {
        [Dependency]
        public SessionContext SessionContext { get; set; }

        [Dependency]
        public CookieContext CookieContext { get; set; }
    }
}