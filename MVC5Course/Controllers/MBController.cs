using MVC5Course.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        // GET: MB
        public ActionResult Index()
        {
            ViewData["Key1"] = "1";
            ViewBag.key2 = "2"; //ViewData的另一種寫法

            //TempData["key3"] = "3";

            ViewData.Model = "4";

            return View();
        }

        public ActionResult WriteTemp()
        {
            TempData["key3"] = "3";
            Session["key5"] = "5";
            return RedirectToAction("Index");
        }

        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test(DepartmentCreationVM data)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            //ModelState.Clear();
            ModelState["Budget"].Errors.Clear();
            return View(data);
        }
    }
}