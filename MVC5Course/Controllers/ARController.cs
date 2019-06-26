using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return PartialView();
        }

        public ActionResult ContentTest()
        {
            return Content("I'am Test");
        }

        //解決IE快取問題
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult DataToJson()
        {
            return Json(
            new
            {
                id = 1,
                name = "喜洋洋"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}