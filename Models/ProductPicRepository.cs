using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nepton.Models
{
    public class ProductPicRepository : IProductPicRepository
    {
        private readonly Entities db = new Entities();
        public IQueryable<NT_ProductPic> FindAll()
        {
            return db.NT_ProductPic;
        }

        public NT_ProductPic GetProductPicById(Guid Id)
        {
            return db.NT_ProductPic.Where(p=>p.PicID==Id).FirstOrDefault();
        }

        public void AddProductPic(NT_ProductPic item)
        {
            item.PicID = Guid.NewGuid();
            db.AddToNT_ProductPic(item);
            db.SaveChanges();
        }

        public bool DeleteProductPic(Guid Id)
        {
            var item = db.NT_ProductPic.Where(p => p.PicID == Id).FirstOrDefault(); ;
            if (item != null) {
                db.DeleteObject(item);
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public void SaveChange()
        {
            db.SaveChanges();
        }


        public IQueryable<NT_ProductPic> GetProductPicByArticleId(Guid ArticleId)
        {
            return db.NT_ProductPic.Where(p => p.NT_Article.ArticleID == ArticleId);
        }
    }
}