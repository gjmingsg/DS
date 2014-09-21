using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nepton.Models;
using MvcPaging;
using System.Net.Mail;
namespace Nepton.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly Guid CompanyArticleId = Guid.Parse("B421878F-FE03-4048-B8EB-01D3CC12B3FD");
        private readonly Guid JoinUsArticleId = Guid.Parse("C0815BC2-D461-4F73-93EA-4D6A5C6D94D5");
        private readonly Guid CultureArticleId = Guid.Parse("39BE8736-CBC0-4894-B75F-687E4C48A94D");
        private readonly Guid NewsId = Guid.Parse("9B647655-9C50-4222-B10F-0B7AE2186701");
        private readonly Guid VideoId = Guid.Parse("9B647655-9C50-4222-B10F-0B7AE2186702");
        //红酒
        public readonly static Guid RedId = Guid.Parse("9B647655-9C50-4222-B10F-0B7AE2186708");
        //白兰地
        public readonly static Guid WhiteId = Guid.Parse("9B647655-9C50-4222-B10F-0B7AE2186707");

        private readonly IContactRepository contactdb = new ContactRepository();
        private readonly IConfigRepository configdb = new ConfigRepository();
        private readonly IArticleRepository articledb = new ArticleRepository();
        private readonly IArticleTypeRepository articleTdb = new ArticleTypeRepository();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController)); 

        public ActionResult Index()
        {
            try
            {
                var list = configdb.FindAll().Where(p => p.Key.Equals("Link")).Select(t => new SelectListItem { Text = t.Value1, Value = t.Value2 }).ToList();
                list.Add(new SelectListItem() { Text = "友情链接", Value = "#", Selected = true });
                ViewData["Link"] = list;

                ViewData["News"] = articledb.FindAll().Where(p => p.NT_ArticleType.TypeID == NewsId).OrderByDescending(p => p.CreateTime).Take(5).ToList();
                ViewData["Video"] = articledb.FindAll().Where(p => p.NT_ArticleType.TypeID == VideoId).OrderByDescending(p => p.CreateTime).First().Content;

                ViewData["ScrollPic"] = configdb.FindAll().Where(p => p.Key == "ScrollPic").ToList();
                SetContactInfoView();
            }
            catch (Exception e) {
                log.Error("首页index出现异常", e);
            }
            return View();
        }

        
        public ActionResult Company() {
            return View(articledb.GetArticleById(CompanyArticleId));
        }
        public ActionResult Join()
        {
            return View(articledb.GetArticleById(JoinUsArticleId));
        }
        public ActionResult ProductList(Guid? TypeID,int? page)
        {
            var red = articleTdb.FindAll().Where(p => p.ParentID == RedId).ToList();
            var white = articleTdb.FindAll().Where(p => p.ParentID == WhiteId).ToList();
            ViewData["RedTypeList"] = red;
            ViewData["WhiteTypeList"] = white;
            var tr = from x in red select x.TypeID;
            var tw = from x in white select x.TypeID;
            
            if (TypeID.HasValue) {
                var list = articledb.FindAll().Where(p => p.NT_ArticleType.TypeID == TypeID.Value).OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10);
                ViewData["TypeID"] = TypeID.Value;
                return View(list);
            }
            else {
                var t= articledb.FindAll();
              
               
                var list = t.Where(p => p.NT_ArticleType.TypeID == RedId 
                    || p.NT_ArticleType.TypeID==WhiteId
                    || tr.Contains(p.NT_ArticleType.TypeID)
                    || tw.Contains(p.NT_ArticleType.TypeID));
               
                return View(list.OrderByDescending(p => p.CreateTime)
                    .ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
            }
            
        }
        public ActionResult ProductDetail(Guid id)
        {
            return View(articledb.GetArticleById(id));
        }
        public ActionResult Contact()
        {
            var list = configdb.FindAll();
            ViewData["Address"] = list.Where(p => p.Key.Equals("Address")).FirstOrDefault().Value1;
            ViewData["Phone"] = list.Where(p => p.Key.Equals("Phone")).FirstOrDefault().Value1;
            ViewData["Mail"] = list.Where(p => p.Key.Equals("Mail")).FirstOrDefault().Value1;
            return View();
        }
        
        [HttpPost]
        public ActionResult Contact(
            [Bind(Exclude = "ContactID,CreateTime,Status")]
            NT_Contact item)
        {
            if (ModelState.IsValid) {
              
                contactdb.AddContact(item);
                ///发邮件给邮箱
                try {
                    var sc = new SmtpClient("localhost");
                    var email = configdb.FindAll().Where(p => p.Key == "Mail").First().Value1;
                    sc.Send(item.Email, email, item.Msg, item.Msg);
                }catch(Exception e){
                    log.Error("添加留言出错", e);
                }
                return RedirectToAction("ContactMsg");
            }
             
            SetContactInfoView();
            return View();
        }
        public ActionResult ContactMsg() {

            ViewData["msg"] = "留言我们已经收到，谢谢你的留言！";
            return View();
        }
        public ActionResult Culture()
        {
            return View(articledb.GetArticleById(CultureArticleId));
        }

        public ActionResult News(Guid id)
        {
            return View(articledb.GetArticleById(id));
        }

        public ActionResult NewsMore(int? page)
        {
            return View(articledb.FindAll().Where(p => p.NT_ArticleType.TypeID == NewsId).OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
        }

        public ActionResult Video(int? page)
        {
            return View(articledb.FindAll().Where(p => p.NT_ArticleType.TypeID == VideoId).OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
        }

        private void SetContactInfoView() {
            var list = configdb.FindAll();
            ViewData["Address"] = list.Where(p => p.Key.Equals("Address")).FirstOrDefault().Value1;
            ViewData["Phone"] = list.Where(p => p.Key.Equals("Phone")).FirstOrDefault().Value1;
            ViewData["Mail"] = list.Where(p => p.Key.Equals("Mail")).FirstOrDefault().Value1;
            ViewData["Tel"] = list.Where(p => p.Key.Equals("Tel")).FirstOrDefault().Value1;
        }
    }
}
