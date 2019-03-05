using System;
using System.Collections.Generic;
using System.Linq;
using Kastra.Module.Article.Business.Contracts;
using Kastra.Module.Article.DAL;
using Kastra.Module.Article.DTO;
using Kastra.Module.Article.DTO.Mappers;

namespace Kastra.Module.Article.Business
{
    public class ArticleBusiness : IArticleBusiness
    {
        private ArticleContext _dbContext = null;

        public ArticleBusiness(ArticleContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<ArticleInfo> GetArticlesList(Int32 moduleId)
        {
            IList<ArticleInfo> articlesInfo = null;
            IList<KastraArticles> articles = _dbContext.KastraArticles.Where(a => a.ModuleId == moduleId).ToList();

            articlesInfo = new List<ArticleInfo>(articles.Count);

            foreach (KastraArticles article in articles)
                articlesInfo.Add(article.ToArticleInfo());

            if (articles == null)
                return null;

            return articlesInfo;
        }

        public ArticleInfo GetArticle(Int32 articleId)
        {
            KastraArticles article = _dbContext.KastraArticles.SingleOrDefault(a => a.ArticleId == articleId);

            if (article == null)
                return null;
            
            return article.ToArticleInfo();
        }

        public void SaveArticle(ArticleInfo articleInfo)
        {
            KastraArticles article = null;

            if (articleInfo.ArticleId > 0)
                article = _dbContext.KastraArticles.SingleOrDefault(a => a.ArticleId == articleInfo.ArticleId);

            if (article == null)
                article = new KastraArticles();

            article.ArticleId = articleInfo.ArticleId;
            article.ArticleContent = articleInfo.ArticleContent;
            article.ArticleOrder = articleInfo.ArticleOrder;
            article.CreatedAt = articleInfo.CreatedAt;
            article.CreatedBy = articleInfo.CreatedBy;
            article.ImageUrl = articleInfo.ImageUrl;
            article.Title = articleInfo.Title;
            article.UpdatedAt = articleInfo.UpdatedAt;
            article.UpdatedBy = articleInfo.UpdatedBy;
            article.UserId = articleInfo.UserId;
            article.ModuleId = articleInfo.ModuleId;

            if(article.ArticleId > 0)
                _dbContext.KastraArticles.Update(article);
            else
                _dbContext.KastraArticles.Add(article);

            _dbContext.SaveChanges();
        }

        public void DeleteArticle(Int32 articleId)
        {
            KastraArticles article = _dbContext.KastraArticles.SingleOrDefault(a => a.ArticleId == articleId);

            if(article != null)
            {
                _dbContext.KastraArticles.Remove(article);
                _dbContext.SaveChanges();
            }  
        }
    }
}