using System;
using System.Web.Mvc;
using Helper;
using log4net;

namespace PlatformSite.Controllers
{
    public class HomeController : BaseController
    {
        private ILog logger = LogManager.GetLogger(typeof (HomeController));
        public ActionResult Index()
        {
            logger.Debug("1. Debug");
            logger.Info("2. Info");
            logger.Warn("3. Warn");
            logger.Error("4. Error");
            logger.Fatal("5. Fatal");
            try
            {
                throw new ArgumentNullException("UserName");
            }
            catch (Exception ex)
            {
                throw new Exception("Index异常", ex);
            }
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