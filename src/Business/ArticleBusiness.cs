using System;
using System.Collections.Generic;
using System.Linq;
using Kastra.Core.Dto;
using Kastra.Module.Article.Business.Contracts;
using Kastra.Module.Article.DAL;
using Kastra.Module.Article.DTO;
using Kastra.Module.Article.DTO.Mappers;
using Microsoft.AspNetCore.Identity;

namespace Kastra.Module.Article.Business
{
    public class ArticleBusiness : IArticleBusiness
    {
        private ArticleContext _dbContext = null;
        private readonly UserManager<ApplicationUser> _userManager;

        public ArticleBusiness(ArticleContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public int CountArticles(int moduleId)
        {
            return _dbContext.KastraArticles.Count(a => a.ModuleId == moduleId);
        }

        public IList<ArticleInfo> GetArticlesList(int moduleId)
        {
            IList<ArticleInfo> articlesInfo = null;
            IList<KastraArticles> articles = _dbContext.KastraArticles.Where(a => a.ModuleId == moduleId).ToList();

            if (articles == null)
            {
                return null;
            }

            articlesInfo = new List<ArticleInfo>(articles.Count);

            foreach (KastraArticles article in articles)
            {
                articlesInfo.Add(article.ToArticleInfo());
            }

            SetAuthorName(articlesInfo);

            return articlesInfo;
        }

        public IList<ArticleInfo> GetArticlesList(int moduleId, int skip, int take)
        {
            IList<ArticleInfo> articlesInfo = null;
            IList<KastraArticles> articles = null;

            articles = _dbContext.KastraArticles.Where(a => a.ModuleId == moduleId)
                                                .Skip(skip)
                                                .Take(take)
                                                .ToList();

            if (articles == null)
            {
                return null;
            }

            articlesInfo = new List<ArticleInfo>(articles.Count);

            foreach (KastraArticles article in articles)
            {
                articlesInfo.Add(article.ToArticleInfo());
            }

            SetAuthorName(articlesInfo);

            return articlesInfo;
        }

        public ArticleInfo GetArticle(Int32 articleId)
        {
            KastraArticles article = _dbContext.KastraArticles.SingleOrDefault(a => a.ArticleId == articleId);

            if (article == null)
                return null;
            
            ArticleInfo articleInfo = article.ToArticleInfo();

            SetAuthorName(articleInfo);
            
            return articleInfo;
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

        #region Private methods

        private void SetAuthorName(IList<ArticleInfo> articles)
        {
            if (articles == null)
            {
                return;
            }

            string[] userIds = articles.Select(a => a.UpdatedBy).ToArray();

            Dictionary<string, string> users = _userManager.Users
                                                .Where(u => userIds.Contains(u.Id))
                                                .ToDictionary(u => u.Id, u => u.DisplayedName);
            
            foreach (ArticleInfo article in articles)
            {
                if (users.ContainsKey(article.UpdatedBy))
                {
                    article.AuthorName = users[article.UpdatedBy];
                }
            }
        }

        private void SetAuthorName(ArticleInfo article)
        {
            if (article == null)
            {
                return;
            }

            string userId = article.UpdatedBy;

            string user = _userManager.Users.SingleOrDefault(u => u.Id == userId)?.DisplayedName;

            if (user != null)
            {
                article.AuthorName = user;
            }
        }

        #endregion
    }
}