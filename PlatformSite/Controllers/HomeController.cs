using System.Web.Mvc;
using Helper;

namespace PlatformSite.Controllers
{
    public class HomeController : BaseController
    {
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