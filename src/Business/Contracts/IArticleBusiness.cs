using System;
using System.Collections.Generic;
using Kastra.Module.Article.DTO;

namespace Kastra.Module.Article.Business.Contracts
{
    public interface IArticleBusiness
    {
        int CountArticles(int moduleId);
        IList<ArticleInfo> GetArticlesList(int moduleId);
        IList<ArticleInfo> GetArticlesList(int moduleId, int skip, int take);
        ArticleInfo GetArticle(int articleId);
        void SaveArticle(ArticleInfo article);
        void DeleteArticle(int articleId);
    }
}
