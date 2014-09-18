using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nepton.Models
{
    interface IArticleRepository
    {
        IQueryable<NT_Article> FindAll();

        NT_Article GetArticleById(Guid Id);

        void AddArticle(NT_Article item);

        bool DeleteArticle(Guid Id);

        void SaveChange();

        NT_ArticleType GetArticleType(Guid Id);
    }
}
