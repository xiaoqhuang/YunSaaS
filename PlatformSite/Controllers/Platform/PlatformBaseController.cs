using System.Web.Mvc;
using Data.Model.Platform;
using Microsoft.Practices.Unity;
using PlatformSite.App_Data;

namespace PlatformSite.Controllers.Platform
{
    public class PlatformBaseController : Controller
    {
        [Dependency]
        public SessionContext SessionContext { protected get; set; }
    }
}