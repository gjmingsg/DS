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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ArticleController)); 
        [Authorize]
        public ActionResult Index(int? page, Guid? TypeID)
        {
            if (TypeID.HasValue)
            {
                ViewData["TypeName"] = typeDb.GetArticleTypeById(TypeID.Value).TypeName;
                ViewData["TypeID"] = TypeID.Value;
                return View(db.FindAll().Where(p => p.NT_ArticleType.TypeID == TypeID).OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
            }
            else
                return View(db.FindAll().OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
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
            [Bind(Exclude = "ArticleID,CreateTime,Creator")]
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

                    item.NT_ArticleType = db.GetArticleType(TypeID);
                    db.AddArticle(item);

                }
                return RedirectToAction("Index", new { TypeID = TypeID });
            }
            catch (Exception e)
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
            var list = new List<SelectListItem>() { { new SelectListItem() { Text = "发布", Value = "2" } }, { new SelectListItem() { Text = "草稿", Value = "1" } } };
            string status = item.Status.Value.ToString();
            list.Where(p => p.Value == status).First().Selected = true;
            ViewData["ddStatus"] = list;
            return View(item);
        }

        //
        // POST: /Article/Edit/5
        [ValidateInput(false)]
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Guid TypeID, Guid ArticleID, [Bind(Exclude = "CreateTime,Creator")]
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
            catch(Exception e)
            {
                log.Error("编辑文章出错", e);
                return View();
            }
        }

        //
        // GET: /Article/Delete/5
        [Authorize]
        [HttpGet]
        public ActionResult Delete(Guid ArticleID,Guid TypeID)
        {
            db.DeleteArticle(ArticleID);
            return RedirectToAction("Index", new { TypeID = TypeID });
        }

        [Authorize]
        public ActionResult ProductList(Guid? TypeID, int? page)
        {
            var red = typeDb.FindAll().Where(p => p.ParentID == HomeController.RedId).ToList();
            var white = typeDb.FindAll().Where(p => p.ParentID == HomeController.WhiteId).ToList();
            
            var tr = from x in red select x.TypeID;
            var tw = from x in white select x.TypeID;

            if (TypeID.HasValue)
            {
                var list = db.FindAll().Where(p => p.NT_ArticleType.TypeID == TypeID.Value).OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10);
                ViewData["TypeID"] = TypeID.Value;
                return View(list);
            }
            else
            {
                var t = db.FindAll();


                var list = t.Where(p => p.NT_ArticleType.TypeID == HomeController.RedId
                    || p.NT_ArticleType.TypeID == HomeController.WhiteId
                    || tr.Contains(p.NT_ArticleType.TypeID)
                    || tw.Contains(p.NT_ArticleType.TypeID));

                return View(list.OrderByDescending(p => p.CreateTime)
                    .ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
            }

        }

        [Authorize]
        [HttpGet]
        public ActionResult ProductDelete(Guid ArticleID)
        {
            db.DeleteArticle(ArticleID);
            return RedirectToAction("ProductList");
        }
        //
        // GET: /Article/Create
        [Authorize]
        public ActionResult ProductCreate()
        {

            ViewData["ddStatus"] = new List<SelectListItem>() { { new SelectListItem() { Text = "发布", Value = "2" } }, { new SelectListItem() { Text = "草稿", Value = "1" } } };
            ViewData["Kind"] = new List<SelectListItem>() { { new SelectListItem() { Text = "红酒", Value = HomeController.RedId.ToString(),Selected=true }}
                                                              ,{ new SelectListItem() { Text = "白兰地", Value = HomeController.WhiteId.ToString() } } };
            var list = typeDb.FindAll().Where(p => p.ParentID == HomeController.RedId).ToList();

            ViewData["TypeID"] = (from x in list select new SelectListItem() { Text = x.TypeName, Value = x.TypeID.ToString() }).ToList();
            return View();
        }

        //
        // POST: /Article/Create
        [ValidateInput(false)]
        [HttpPost]
        [Authorize]
        public ActionResult ProductCreate(
            [Bind(Exclude = "ArticleID,CreateTime,Creator")]
            NT_Article item, Guid TypeID)
        {
             
            try
            {
                if (ModelState.IsValid)
                {
                    
                    item.ArticleID = Guid.NewGuid();
                    item.CreateTime = DateTime.Now;
                    item.Creator = User.Identity.Name;
                     
                    item.NT_ArticleType = db.GetArticleType(TypeID);
                    db.AddArticle(item);

                }
                return RedirectToAction("ProductList");
            }
            catch (Exception e)
            {
                return View(e);
            }
        }

        //
        // GET: /Article/Edit/5
        [Authorize]
        public ActionResult ProductEdit(Guid ArticleID)
        { 
            var item = db.GetArticleById(ArticleID);
            var list = new List<SelectListItem>() { { new SelectListItem() { Text = "发布", Value = "2" } }
                                                  , { new SelectListItem() { Text = "草稿", Value = "1" } } };
            string status = item.Status.Value.ToString();
            list.Where(p => p.Value == status).First().Selected = true;
            ViewData["ddStatus"] = list;

            var TypeID = item.NT_ArticleType.TypeID;
            var ParentID = item.NT_ArticleType.ParentID;
            var kind = new List<SelectListItem>() { { new SelectListItem() { Text = "红酒", Value = HomeController.RedId.ToString() }}
                                                              ,{ new SelectListItem() { Text = "白兰地", Value = HomeController.WhiteId.ToString() } } };
            var tParentID = ParentID.ToString();
            kind.Where(p => p.Value == tParentID).First().Selected = true;
            ViewData["Kind"] = kind;

            var tTypeID = TypeID.ToString();
            var typeList = new SelectList(typeDb.FindAll().Where(p=>p.ParentID==ParentID), "TypeID", "TypeName");
            typeList.Where(p => p.Value == tTypeID).First().Selected = true;
            ViewData["TypeID"] = typeList;
            return View(item);
        }
       [Authorize]
        public ActionResult ProductType(Guid kind) {
            return Json(typeDb.FindAll().Where(p => p.ParentID == kind).ToList(),JsonRequestBehavior.AllowGet);   
        }

        //
        // POST: /Article/Edit/5
        [ValidateInput(false)]
        [HttpPost]
        [Authorize]
        public ActionResult ProductEdit( Guid ArticleID, [Bind(Exclude = "CreateTime,Creator")]
            NT_Article item, Guid TypeID)
        {

            try
            {
                
                var t = db.GetArticleById(ArticleID);
                t.Content = item.Content;
                t.Status = item.Status;
                t.Title = item.Title;
                t.NT_ArticleType = db.GetArticleType(TypeID);
                db.SaveChange();
                return RedirectToAction("ProductList");
            }
            catch
            {
                return View();
            }
        }

        

    }
}
