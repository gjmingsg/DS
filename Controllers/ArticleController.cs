using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using Nepton.Models;
namespace Nepton.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /Article/
        private readonly IArticleRepository db = new ArticleRepository();
        private readonly IArticleTypeRepository typeDb = new ArticleTypeRepository();
        [Authorize]
        public ActionResult Index(int? page, Guid? TypeID)
        {
            
            var list = db.FindAll().Where(p => p.NT_ArticleType.TypeID == TypeID);
            
            if (TypeID.HasValue)
            {
                ViewData["TypeName"] = typeDb.GetArticleTypeById(TypeID.Value).TypeName;
                ViewData["TypeID"] = TypeID.Value;
                return View(list.OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
            }
            else
                return View();
        }

        //
        // GET: /Article/Details/5
        [Authorize]
        public ActionResult Details(Guid ArticleID)
        {
            return View(db.GetArticleById(ArticleID));
        }

        //
        // GET: /Article/Create
        [Authorize]
        public ActionResult Create(Guid TypeID)
        {
            ViewData["TypeName"] = typeDb.GetArticleTypeById(TypeID).TypeName;
            ViewData["TypeID"] = TypeID;
            var list = new List<SelectListItem>() { { new SelectListItem() { Text = "发布", Value = "2" } }, { new SelectListItem() { Text = "草稿", Value = "1" } } };
            ViewData["ddStatus"] = list;
            return View();
        } 

        //
        // POST: /Article/Create
        [ValidateInput(false)]
        [HttpPost]
        [Authorize]
        public ActionResult Create(Guid TypeID,
            [Bind( Exclude = "ArticleID,CreateTime,Creator")]
            NT_Article item)
        {
            ViewData["TypeID"] = TypeID;
            ViewData["TypeName"] = typeDb.GetArticleTypeById(TypeID).TypeName;
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    item.ArticleID = Guid.NewGuid();
                    item.CreateTime = DateTime.Now;
                    item.Creator = User.Identity.Name;

                    item.NT_ArticleType = db.GetArticleType(TypeID) ;
                    db.AddArticle(item);
                   
                }
                return RedirectToAction("Index", new { TypeID = TypeID });
            }
            catch(Exception e)
            {
                return View(e);
            }
        }
        
        //
        // GET: /Article/Edit/5
        [Authorize]
        public ActionResult Edit(Guid ArticleID, Guid TypeID)
        {
            ViewData["TypeName"] = typeDb.GetArticleTypeById(TypeID).TypeName;
            ViewData["TypeID"] = TypeID;
            var item = db.GetArticleById(ArticleID);
            var list = new List<SelectListItem>() { { new SelectListItem() { Text = "发布", Value = "2" } }, { new SelectListItem() { Text = "草稿", Value = "1" } }};
            string status = item.Status.Value.ToString();
            list.Where(p => p.Value == status).First().Selected=true;
            ViewData["ddStatus"] = list;
            return View(item);
        }

        //
        // POST: /Article/Edit/5
        [ValidateInput(false)]
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Guid TypeID,Guid ArticleID,[Bind(Exclude = "CreateTime,Creator")]
            NT_Article item)
        {
            ViewData["TypeName"] = typeDb.GetArticleTypeById(TypeID).TypeName;
            ViewData["TypeID"] = TypeID;
            
            try
            {
                // TODO: Add update logic here
                var t = db.GetArticleById(ArticleID);
                t.Content = item.Content;
                t.Status = item.Status;
                t.Title = item.Title;
                db.SaveChange();
                return RedirectToAction("Index", new { TypeID = TypeID });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Article/Delete/5
        [Authorize]
        public ActionResult Delete(Guid ArticleID)
        {
            db.DeleteArticle(ArticleID);
            return View();
        }

        private string ConvertStatus(int? status) {
            if (status.Value == 1)
                return "草稿";
            else
                return "发布";
        }
    }
}
