using AsmAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsmAD.Controllers
{
    public class TopicController : Controller
    {
        // GET: Topic
        public ActionResult Index(string strSearch)
        {
            TopicList tList = new TopicList();
            List<TopicClass> obj = tList.GetTopicClasses(string.Empty).OrderBy(x => x.Id_Topic).ToList();
            if (!string.IsNullOrEmpty(strSearch))
            {
                obj = obj.Where(x => x.Name.Contains(strSearch) || x.Course.Contains(strSearch) || x.Trainer.Contains(strSearch)).ToList();
            }
            @ViewBag.strSearch = strSearch;
            return View(obj);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TopicClass t)
        {
            if (ModelState.IsValid)
            {
                TopicList tList = new TopicList();
                tList.AddTopic(t);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(string id = null)
        {
            TopicList tList = new TopicList();
            List<TopicClass> obj = tList.GetTopicClasses(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(TopicClass t)
        {
            TopicList tList = new TopicList();
            tList.UpdateTopic(t);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id = null)
        {
            TopicList tList = new TopicList();
            List<TopicClass> obj = tList.GetTopicClasses(id);
            return View(obj.FirstOrDefault());
        }

        public ActionResult Delete(string id = null)
        {
            TopicList tList = new TopicList();
            List<TopicClass> obj = tList.GetTopicClasses(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(TopicClass t)
        {
            TopicList tList = new TopicList();
            tList.DeleteAccount(t);
            return RedirectToAction("Index");
        }
    }
}