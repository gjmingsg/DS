using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nepton.Models;
using MvcPaging;
namespace Nepton.Controllers
{
    [Authorize]
    public class ConfigController : Controller
    {
        //
        // GET: /Config/
        private readonly IConfigRepository db = new ConfigRepository();
        public ActionResult Index(int? page)
        {
            var list = db.FindAll().OrderByDescending(p=>p.Key).ToPagedList(page.HasValue?page.Value-1:0,10);
            return View(list);
        }

        //
        // GET: /Config/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Config/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Config/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Config/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Config/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Config/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            db.DeleteConfig(id);
            return RedirectToAction("Index");
        }

        //
        // POST: /Config/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                db.DeleteConfig(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
