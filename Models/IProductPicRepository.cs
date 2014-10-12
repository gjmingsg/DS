using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nepton.Models
{
    interface IProductPicRepository
    {
        IQueryable<NT_ProductPic> FindAll();

        NT_ProductPic GetProductPicById(Guid Id);

        IQueryable<NT_ProductPic> GetProductPicByArticleId(Guid ArticleId);

        void AddProductPic(NT_ProductPic item);

        bool DeleteProductPic(Guid Id);

        void SaveChange();
        
    }
}