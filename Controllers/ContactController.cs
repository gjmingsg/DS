using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nepton.Models;
using MvcPaging;
namespace Nepton.Controllers
{
    public class ContactController : Controller
    {
        private IContactRepository db = new ContactRepository();
        //
        // GET: /Contact/
        [Authorize]
        public ActionResult Index(int? page)
        {
            return View(db.FindAll().OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
        }

        //
        // GET: /Contact/Details/5
        [Authorize]
        public ActionResult Details(Guid id)
        {
            return View(db.GetContactById(id));
        }

        
        //
        // GET: /Contact/Delete/5
        [Authorize]
        public ActionResult Delete(Guid id)
        {
            db.DeleteContact(id);
            return RedirectToAction("Index");
        }

    }
}
