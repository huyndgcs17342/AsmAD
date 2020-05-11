using AsmAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsmAD.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index(string strSearch)
        {
            CourseList cList = new CourseList();
            List<CourseClass> obj = cList.GetCourseClasses(string.Empty).OrderBy(x => x.Id_Course).ToList();
            if (!string.IsNullOrEmpty(strSearch))
            {
                obj = obj.Where(x => x.Name.Contains(strSearch)).ToList();
            }
            @ViewBag.strSearch = strSearch;
            return View(obj);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CourseClass c)
        {
            if (ModelState.IsValid)
            {
                CourseList cList = new CourseList();
                cList.AddCourse(c);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(string id = null)
        {
            CourseList cList = new CourseList();
            List<CourseClass> obj = cList.GetCourseClasses(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(CourseClass c)
        {
            CourseList cList = new CourseList();
            cList.UpdateCourse(c);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id = null)
        {
            CourseList cList = new CourseList();
            List<CourseClass> obj = cList.GetCourseClasses(id);
            return View(obj.FirstOrDefault());
        }

        public ActionResult Delete(string id = null)
        {
            CourseList cList = new CourseList();
            List<CourseClass> obj = cList.GetCourseClasses(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(CourseClass c)
        {
            CourseList cList = new CourseList();
            cList.DeleteCourse(c);
            return RedirectToAction("Index");
        }
    }
}