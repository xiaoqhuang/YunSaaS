using System;
using System.Web.Mvc;
using Helper;
using log4net;
using Microsoft.Practices.Unity;
using Service.Platform;

namespace PlatformSite.Controllers
{
    public class HomeController : BaseController
    {
        private ILog logger = LogManager.GetLogger(typeof (HomeController));
        [Dependency]
        internal PlatformUserService UserService { private get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetValidateImage()
        {
            string randomCode;
            byte[] imageByte = ValidateCodeHelper.GenerateValidateGraphic(out randomCode);
            SessionContext.ValidateCode = randomCode;
            return File(imageByte, @"image/jpeg");
        }
    }
}