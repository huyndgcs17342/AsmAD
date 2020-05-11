using AsmAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsmAD.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(string strSearch)
        {
            CategoryList cateList = new CategoryList();
            List<CategoryClass> obj = cateList.GetCategoryClasses(string.Empty).OrderBy(x => x.Id_Cate).ToList();
            if (!string.IsNullOrEmpty(strSearch))
            {
                obj = obj.Where(x => x.Name.Contains(strSearch) || x.Description.Contains(strSearch)).ToList();
            }
            ViewBag.strSearch = strSearch;
            return View(obj);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryClass cate)
        {
            if (ModelState.IsValid)
            {
                CategoryList cateList = new CategoryList();
                cateList.AddCategory(cate);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(string id = null)
        {
            CategoryList cateList = new CategoryList();
            List<CategoryClass> obj = cateList.GetCategoryClasses(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(CategoryClass cate)
        {
            CategoryList cateList = new CategoryList();
            cateList.UpdateCategory(cate);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id = null)
        {
            CategoryList cateList = new CategoryList();
            List<CategoryClass> obj = cateList.GetCategoryClasses(id);
            return View(obj.FirstOrDefault());
        }

        public ActionResult Delete(string id = null)
        {
            CategoryList cateList = new CategoryList();
            List<CategoryClass> obj = cateList.GetCategoryClasses(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(CategoryClass cate)
        {
            CategoryList cateList = new CategoryList();
            cateList.DeleteCategory(cate);
            return RedirectToAction("Index");
        }
    }
}