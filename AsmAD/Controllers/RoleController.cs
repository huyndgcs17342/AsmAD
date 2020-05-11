using AsmAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsmAD.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index(string strSearch)
        {
            RoleList roleList = new RoleList();
            List<RoleClass> obj = roleList.GetRoleClasses(string.Empty).OrderBy(x => x.Id_Role).ToList();
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
        public ActionResult Create(RoleClass role)
        {
            if (ModelState.IsValid)
            {
                RoleList accList = new RoleList();
                accList.AddRole(role);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(string id = null)
        {
            RoleList roleList = new RoleList();
            List<RoleClass> obj = roleList.GetRoleClasses(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(RoleClass role)
        {
            RoleList roleList = new RoleList();
            roleList.UpdateRole(role);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id = null)
        {
            RoleList roleList = new RoleList();
            List<RoleClass> obj = roleList.GetRoleClasses(id);
            return View(obj.FirstOrDefault());
        }

        public ActionResult Delete(string id = null)
        {
            RoleList roleList = new RoleList();
            List<RoleClass> obj = roleList.GetRoleClasses(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(RoleClass role)
        {
            RoleList accList = new RoleList();
            accList.DeleteRole(role);
            return RedirectToAction("Index");
        }
    }
}