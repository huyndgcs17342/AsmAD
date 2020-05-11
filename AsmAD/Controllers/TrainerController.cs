using AsmAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsmAD.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult Index(string strSearch)
        {
            TrainerList tList = new TrainerList();
            List<TrainerClass> obj = tList.GetTrainerClasses(string.Empty).OrderBy(x => x.Id_Trainer).ToList();
            if (!string.IsNullOrEmpty(strSearch))
            {
                obj = obj.Where(x => x.Name.Contains(strSearch) || x.ExOrInType.Contains(strSearch) || x.Topic.Contains(strSearch)).ToList();
            }
            @ViewBag.strSearch = strSearch;
            return View(obj);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TrainerClass t)
        {
            if (ModelState.IsValid)
            {
                TrainerList tList = new TrainerList();
                tList.AddTrainer(t);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(string id = null)
        {
            TrainerList tList = new TrainerList();
            List<TrainerClass> obj = tList.GetTrainerClasses(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(TrainerClass t)
        {
            TrainerList tList = new TrainerList();
            tList.UpdateTrainer(t);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id = null)
        {
            AccountList accList = new AccountList();
            List<AccountClass> obj = accList.GetAccountClass(id);
            return View(obj.FirstOrDefault());
        }

        public ActionResult Delete(string id = null)
        {
            TrainerList tList = new TrainerList();
            List<TrainerClass> obj = tList.GetTrainerClasses(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(TrainerClass t)
        {
            TrainerList tList = new TrainerList();
            tList.DeleteTrainer(t);
            return RedirectToAction("Index");
        }
    }
}