using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nepton.Models
{
    interface IArticleTypeRepository
    {
        NT_ArticleType GetArticleTypeById(Guid Id);
        IQueryable<NT_ArticleType> FindAll();
    }
}
