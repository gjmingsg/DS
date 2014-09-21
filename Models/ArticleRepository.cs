using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nepton.Models
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly Entities db = new Entities();
        public IQueryable<NT_Article> FindAll()
        {
            return db.NT_Article.Include("NT_ArticleType");
        }

        public NT_Article GetArticleById(Guid Id)
        {
            return db.NT_Article.Include("NT_ArticleType").Where(p => p.ArticleID == Id).FirstOrDefault();
        }

        public void AddArticle(NT_Article item)
        {
            db.AddToNT_Article(item);
            db.SaveChanges();
        }

        public bool DeleteArticle(Guid Id)
        {
            var item = db.NT_Article.Where(p => p.ArticleID == Id).FirstOrDefault();
            if (item != null)
            {
                db.DeleteObject(item);
                db.SaveChanges();
                return true;
            }
            else {
                return false;
            }
        }


        public void SaveChange()
        {
            db.SaveChanges();
        }

        public NT_ArticleType GetArticleType(Guid Id) {

            return db.NT_ArticleType.Where(p => p.TypeID == Id).FirstOrDefault();
        }

    }
}