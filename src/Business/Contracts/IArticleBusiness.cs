using System;
using System.Collections.Generic;
using Kastra.Module.Article.DTO;

namespace Kastra.Module.Article.Business.Contracts
{
    public interface IArticleBusiness
    {
        IList<ArticleInfo> GetArticlesList(Int32 moduleId);
        ArticleInfo GetArticle(Int32 articleId);
        void SaveArticle(ArticleInfo article);
        void DeleteArticle(Int32 articleId);
    }
}
