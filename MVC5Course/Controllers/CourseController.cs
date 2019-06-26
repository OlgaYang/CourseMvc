using MVC5Course.Models;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using X.PagedList;

namespace MVC5Course.Controllers
{
    [RoutePrefix("Courses")]
    [HandleError(View = "Error.TestException",
        ExceptionType = typeof(ArgumentException))]
    public class CourseController : Controller
    {
        private CourseRepository repo;
        private DepartmentRepository repoDept;

        public CourseController()
        {
            repo = RepositoryHelper.GetCourseRepository();
            repoDept = RepositoryHelper.GetDepartmentRepository(repo.UnitOfWork);
        }

        // GET: Courses
        [Route("Index")]
        public ActionResult Index(int pageNo = 1, int pageSize = 1)
        {
            var course = repo.All().OrderBy(p=>p.CourseID).ToPagedList(pageNo, pageSize);
            return View(course);
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = repo.Find(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(repoDept.All(), "DepartmentID", "Name");
            return View();
        }

        // POST: Courses/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Credits,DepartmentID,Location")] Course course)
        {
            if (ModelState.IsValid)
            {
                repo.Add(course);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(repoDept.All(), "DepartmentID", "Name", course.DepartmentID);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = repo.Find(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(repoDept.All(), "DepartmentID", "Name", course.DepartmentID);
            return View(course);
        }

        // POST: Courses/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            Course course = repo.Find(id);

            //延遲驗證 Model Binding
            if (TryUpdateModel<IEditCourse>(course) && TryValidateModel(course)) //Include
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            //if (ModelState.IsValid)
            //{
            //    repo.UnitOfWork.Context.Entry(course).State = EntityState.Modified;
            //    repo.UnitOfWork.Commit();
            //    return RedirectToAction("Index");
            //}
            ViewBag.DepartmentID = new SelectList(repoDept.All(), "DepartmentID", "Name", course.DepartmentID);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = repo.Find(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = repo.Find(id);
            repo.Delete(course);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        [Route("list/{deptId}")]
        //[ChildActionOnly]
        public ActionResult List(int deptId)
        {
            var course = repo.All().Where(p => p.DepartmentID == deptId);
            return PartialView(course.ToList());
        }

        [Route("GetTime")]
        public ActionResult GetTime()
        {
            return Content(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}