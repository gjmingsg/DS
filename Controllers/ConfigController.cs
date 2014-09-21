using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nepton.Models;
using MvcPaging;
namespace Nepton.Controllers
{
   
    public class ConfigController : Controller
    {
        //
        // GET: /Config/
        private readonly IConfigRepository db = new ConfigRepository();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ConfigController)); 
        /// <summary>
        /// 友情链接
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult LinkIndex(int? page)
        {
            var list = db.FindAll().Where(p => p.Key == "Link").OrderByDescending(p => p.Key).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10);
            ViewData["index"] = "LinkIndex";
            return View("index",list);
        }
        /// <summary>
        /// 首页轮转图片
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult ScrollPicIndex(int? page)
        {
            var list = db.FindAll().Where(p => p.Key == "ScrollPic").OrderByDescending(p => p.Key).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10);
            ViewData["index"] = "ScrollPicIndex";
            return View("index", list);
        }
        /// <summary>
        /// 其他设置
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult OtherIndex(int? page)
        {
            var list = db.FindAll().Where(p => p.Key != "ScrollPic" && p.Key != "Link").OrderByDescending(p => p.Key).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10);
            ViewData["index"] = "OtherIndex";
            return View("index", list);
        }
        //
        // GET: /Config/Details/5
         [Authorize]
        public ActionResult Details(Guid id, string srcPage)
        {
            ViewData["index"] = srcPage;
            return View(db.GetConfigById(id));
        }

        //
        // GET: /Config/Create
         [Authorize]
         public ActionResult Create(string srcPage)
        {
            ViewData["index"] = srcPage;
            return View();
        } 

        //
        // POST: /Config/Create
         [Authorize]
        [HttpPost]
         public ActionResult Create([Bind(Exclude = "ConfigID")]NT_Config item, string srcPage)
        {
            try
            {
                item.Key = srcPage.Replace("Index", "");
                item.ConfigID = Guid.NewGuid();
                db.AddConfig(item);
                return RedirectToAction(srcPage);
            }
            catch(Exception e)
            {
                log.Error("新建配置项出错", e);
                return View();
            }
        }
        
        //
        // GET: /Config/Edit/5
        [Authorize]
         public ActionResult Edit(Guid id, string srcPage)
        {
            ViewData["index"] = srcPage;
            return View(db.GetConfigById(id));
        }

        //
        // POST: /Config/Edit/5
         [Authorize]
        [HttpPost]
        public ActionResult Edit(Guid id, [Bind(Exclude = "ConfigID")]NT_Config item, string srcPage)
        {
            try
            {
                ViewData["index"] = srcPage;
                var config = db.GetConfigById(id);
                config.Name = item.Name;
                config.Value1 = item.Value1;
                config.Value2 = item.Value2;
                config.Value3 = item.Value3;
                config.Value4 = item.Value4;
                db.SaveChange();
                return RedirectToAction(srcPage);
            }
            catch(Exception e)
            {
                log.Error("编辑配置项出错", e);
                return View();
            }
        }

      
        //
        // POST: /Config/Delete/5
         [Authorize]
        [HttpGet]
        public ActionResult Delete(Guid id,string srcPage)
        {
            try
            {
                // TODO: Add delete logic here
                db.DeleteConfig(id);
                return RedirectToAction(srcPage);
            }
            catch
            {
                return View();
            }
        }
    }
}
