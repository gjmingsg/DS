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
        public readonly static Guid CompanyArticleId = Guid.Parse("B421878F-FE03-4048-B8EB-01D3CC12B3FD");
        public readonly static Guid JoinUsArticleId = Guid.Parse("C0815BC2-D461-4F73-93EA-4D6A5C6D94D5");
        public readonly static Guid CultureArticleId = Guid.Parse("39BE8736-CBC0-4894-B75F-687E4C48A94D");
        public readonly static Guid NewsId = Guid.Parse("9B647655-9C50-4222-B10F-0B7AE2186701");
        public readonly static Guid VideoId = Guid.Parse("9B647655-9C50-4222-B10F-0B7AE2186702");
        /// <summary>
        /// 公告板/专栏
        /// </summary>
        public readonly static Guid BBS = Guid.Parse("9b647655-9c50-4222-b10f-0b7ae2186718");

        //红酒
        public readonly static Guid RedId = Guid.Parse("9B647655-9C50-4222-B10F-0B7AE2186708");
        //白兰地
        public readonly static Guid WhiteId = Guid.Parse("9B647655-9C50-4222-B10F-0B7AE2186707");
        /// <summary>
        /// 场合选酒
        /// </summary>
        public readonly static Guid OccastionId = Guid.Parse("9B647655-9C50-4222-B10F-0B7AE2186715");
        //商务宴请
        public readonly static Guid BusinessId = Guid.Parse("9B647655-9C50-4222-B10F-0B7AE2186716");
        //婚宴推荐
        public readonly static Guid WeddingId = Guid.Parse("9B647655-9C50-4222-B10F-0B7AE2186717");

        private readonly IContactRepository contactdb = new ContactRepository();
        private readonly IConfigRepository configdb = new ConfigRepository();
        private readonly IArticleRepository articledb = new ArticleRepository();
        private readonly IArticleTypeRepository articleTdb = new ArticleTypeRepository();
        private readonly IProductPicRepository prodDb = new ProductPicRepository();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController)); 
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
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
                ViewData["BBS"] = articledb.FindAll().Where(p => p.NT_ArticleType.TypeID == BBS).OrderByDescending(p=>p.CreateTime).Take(3).ToList();
                SetContactInfoView();
            }
            catch (Exception e) {
                log.Error("首页index出现异常", e);
            }
            return View();
        }
        /// <summary>
        /// 场合选酒
        /// </summary>
        /// <param name="TypeID">场合类型ID</param>
        /// <param name="page">第几页</param>
        /// <returns></returns>
        public ActionResult Occastion(Guid? TypeID,int? page)
        {

            if (TypeID.HasValue)
            {
                var typeName = articleTdb.GetArticleTypeById(TypeID.Value);
                ViewData["TypeName"] = typeName.TypeName;
                return View(articledb.FindAll().Where(p => p.NT_ArticleType.TypeID == TypeID.Value).OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
            }
            else
            {
                var typeName = articleTdb.GetArticleTypeById(OccastionId);
                ViewData["TypeName"] = typeName.TypeName;
                return View(articledb.FindAll().Where(p => p.NT_ArticleType.ParentID == OccastionId).OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
            }
        }
        /// <summary>
        /// 场合选酒明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OccastionDetail(Guid id)
        {
            return View(articledb.GetArticleById(id));
        }
        /// <summary>
        /// 公司介绍
        /// </summary>
        /// <returns></returns>
        public ActionResult Company() {
            return View(articledb.GetArticleById(CompanyArticleId));
        }
        /// <summary>
        /// 加入我们
        /// </summary>
        /// <returns></returns>
        public ActionResult Join()
        {
            return View(articledb.GetArticleById(JoinUsArticleId));
        }
        /// <summary>
        /// 产品清单
        /// </summary>
        /// <param name="TypeID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult ProductList(Guid? BigTypeID,Guid? TypeID,int? page)
        {
            var red = articleTdb.FindAll().Where(p => p.ParentID == RedId).ToList();
            var white = articleTdb.FindAll().Where(p => p.ParentID == WhiteId).ToList();
            
            var tr = from x in red select x.TypeID;
            var tw = from x in white select x.TypeID;
           
            ///如果大类有值，则只显示大类的（红酒，白兰地）
            if (BigTypeID.HasValue) {
                ViewData["BigTypeID"] = BigTypeID.Value;
                if(BigTypeID.Value == RedId)
                    ViewData["RedTypeList"] = red;
                else
                    ViewData["WhiteTypeList"] = white;
                ///如果小类有值，显示小类（国家）
                if (TypeID.HasValue)
                {
                    var list = articledb.FindAll().Where(p => p.NT_ArticleType.TypeID == TypeID.Value).OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10);
                    ViewData["TypeID"] = TypeID.Value;
                    var piclist = new List<Dictionary<string, string>>();
                    foreach (var i in list)
                    {
                        var t = prodDb.GetProductPicByArticleId(i.ArticleID).FirstOrDefault();
                        if (t != null)
                        {
                            piclist.Add(new Dictionary<string, string>() { { "ArticleID", i.ArticleID.ToString() }, { "Url", t.Url.ToLower().Replace(".jpg", "_small.jpg") } });
                        }
                    }
                    ViewData["PicList"] = piclist;
                    return View(list);
                }
                else {
                    var bigtypeid = articleTdb.FindAll().Where(p => p.ParentID == BigTypeID.Value).ToList();
                    var bw = from x in bigtypeid select x.TypeID;
                    var t = articledb.FindAll();
                    var list = t.Where(p=>bw.Contains(p.NT_ArticleType.TypeID));

                    var piclist = new List<Dictionary<string, string>>();
                    foreach (var i in list)
                    {
                        var t1 = prodDb.GetProductPicByArticleId(i.ArticleID).FirstOrDefault();
                        if (t1 != null)
                        {
                            piclist.Add(new Dictionary<string, string>() { { "ArticleID", i.ArticleID.ToString() }, { "Url", t1.Url.ToLower().Replace(".jpg", "_small.jpg") } });
                        }
                    }
                    ViewData["PicList"] = piclist;

                    return View(list.OrderByDescending(p => p.CreateTime)
                        .ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
                }
            }
            ///都没有值，则全部显示
            else
            {
                ViewData["RedTypeList"] = red;
                ViewData["WhiteTypeList"] = white;
                var t = articledb.FindAll();
                var list = t.Where(p => 
                    //p.NT_ArticleType.TypeID == RedId ||
                    //p.NT_ArticleType.TypeID == WhiteId ||
                    tr.Contains(p.NT_ArticleType.TypeID)||
                    tw.Contains(p.NT_ArticleType.TypeID));

                var piclist = new List<Dictionary<string, string>>();
                foreach (var i in list)
                {
                    var t1 = prodDb.GetProductPicByArticleId(i.ArticleID).FirstOrDefault();
                    if (t1 != null)
                    {
                        piclist.Add(new Dictionary<string, string>() { { "ArticleID", i.ArticleID.ToString() }, { "Url", t1.Url.ToLower().Replace(".jpg", "_small.jpg") } });
                    }
                }
                ViewData["PicList"] = piclist;

                return View(list.OrderByDescending(p => p.CreateTime)
                    .ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
            }
            
        }
        /// <summary>
        /// 产品明细，页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ProductDetail(Guid id)
        {
            return View(articledb.GetArticleById(id));
        }
        /// <summary>
        /// 产品明细AJAX TODO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ProductDetailAjax(Guid DatailId)
        {
            var a = articledb.GetArticleById(DatailId);
            Dictionary<string,object> item = new Dictionary<string,object>(){
            {"CreateTime",a.CreateTime }
            ,{"Content",a.Content}
            ,{"Creator",a.Creator}
            ,{"Title",a.Title}};
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 联系我们页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            var list = configdb.FindAll();
            ViewData["Address"] = list.Where(p => p.Key.Equals("Address")).FirstOrDefault().Value1;
            ViewData["Phone"] = list.Where(p => p.Key.Equals("Phone")).FirstOrDefault().Value1;
            ViewData["Mail"] = list.Where(p => p.Key.Equals("Mail")).FirstOrDefault().Value1;
            return View();
        }
        /// <summary>
        /// 保存留言信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 留言成功信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ContactMsg() {
            ViewData["msg"] = "留言我们已经收到，谢谢你的留言！";
            return View();
        }
        /// <summary>
        /// 公司文化
        /// </summary>
        /// <returns></returns>
        public ActionResult Culture()
        {
            return View(articledb.GetArticleById(CultureArticleId));
        }
        /// <summary>
        /// 新闻明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult News(Guid id)
        {
            return View(articledb.GetArticleById(id));
        }
        /// <summary>
        /// 更多新闻
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult NewsMore(Guid? TypeID,int? page)
        {
            if(TypeID.HasValue)
                return View(articledb.FindAll().Where(p => p.NT_ArticleType.TypeID == BBS).OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
            return View(articledb.FindAll().Where(p => p.NT_ArticleType.TypeID == NewsId).OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
        }
        /// <summary>
        /// 公司视频明细
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Video(int? page)
        {
            return View(articledb.FindAll().Where(p => p.NT_ArticleType.TypeID == VideoId).OrderByDescending(p => p.CreateTime).ToPagedList(page.HasValue ? page.Value - 1 : 0, 10));
        }
        /// <summary>
        /// 设置联系方式
        /// </summary>
        private void SetContactInfoView() {
            var list = configdb.FindAll();
            ViewData["Address"] = list.Where(p => p.Key.Equals("Address")).FirstOrDefault().Value1;
            ViewData["Phone"] = list.Where(p => p.Key.Equals("Phone")).FirstOrDefault().Value1;
            ViewData["Mail"] = list.Where(p => p.Key.Equals("Mail")).FirstOrDefault().Value1;
            ViewData["Tel"] = list.Where(p => p.Key.Equals("Tel")).FirstOrDefault().Value1;
        }
    }
}
