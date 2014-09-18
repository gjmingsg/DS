using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nepton.Models
{
    public class ArticleTypeRepository:IArticleTypeRepository
    {
        private readonly Entities db = new Entities();
        public NT_ArticleType GetArticleTypeById(Guid Id)
        {
            return db.NT_ArticleType.Where(p => p.TypeID == Id).FirstOrDefault();
        }


        public IQueryable<NT_ArticleType> FindAll()
        {
            return db.NT_ArticleType;
        }
    }
}