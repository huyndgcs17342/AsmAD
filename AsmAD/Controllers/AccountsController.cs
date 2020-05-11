using AsmAD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsmAD.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index(string strSearch)
        {
            RoleClass roleDDL = new RoleClass();
            using (DbModel db=new DbModel())
            {
                roleDDL.Id_Role=db.StoreModel.
            }

            AccountList accList = new AccountList();
            List<AccountClass> obj = accList.GetAccountClass(string.Empty).OrderBy(x => x.Id_Account).ToList();
            if (!string.IsNullOrEmpty(strSearch))
            {
                obj = obj.Where(x => x.Account.Contains(strSearch)|| x.Password.Contains(strSearch)|| x.Role.Contains(strSearch)).ToList();
            }
            @ViewBag.strSearch = strSearch;
            return View(obj);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AccountClass acc)
        {
            if (ModelState.IsValid)
            {
                AccountList accList = new AccountList();
                accList.AddAccount(acc);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(string id = null)
        {
            AccountList accList = new AccountList();
            List<AccountClass> obj = accList.GetAccountClass(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(AccountClass acc)
        {
            AccountList accList = new AccountList();
            accList.UpdateAccount(acc);
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
            AccountList accList = new AccountList();
            List<AccountClass> obj = accList.GetAccountClass(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(AccountClass acc)
        {
            AccountList accList = new AccountList();
            accList.DeleteAccount(acc);
            return RedirectToAction("Index");
        }
    }
}