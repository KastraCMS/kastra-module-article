using System;
using Kastra.Module.Article.DAL;

namespace Kastra.Module.Article.DTO.Mappers
{
    public static class ArticleMapper
    {
        public static ArticleInfo ToArticleInfo(this KastraArticles article)
        {
            ArticleInfo articleInfo = new ArticleInfo();
            articleInfo.ArticleId = article.ArticleId;
            articleInfo.ArticleContent = article.ArticleContent;
            articleInfo.ArticleOrder = article.ArticleOrder;
            articleInfo.CreatedAt = article.CreatedAt;
            articleInfo.ImageUrl = article.ImageUrl;
            articleInfo.Title = article.Title;
            articleInfo.UserId = article.UserId;
            articleInfo.CreatedBy = article.CreatedBy;
            articleInfo.UpdatedAt = article.UpdatedAt;
            articleInfo.UpdatedBy = article.UpdatedBy;
            articleInfo.ModuleId = article.ModuleId;

            return articleInfo;
        }

        public static KastraArticles ToKastraArticle(this ArticleInfo articleInfo)
        {
            KastraArticles article = new KastraArticles();
			article.ArticleId = articleInfo.ArticleId;
			article.ArticleContent = articleInfo.ArticleContent;
			article.ArticleOrder = articleInfo.ArticleOrder;
			article.CreatedAt = articleInfo.CreatedAt;
			article.ImageUrl = articleInfo.ImageUrl;
			article.Title = articleInfo.Title;
			article.UserId = articleInfo.UserId;
			article.CreatedBy = articleInfo.CreatedBy;
			article.UpdatedAt = articleInfo.UpdatedAt;
			article.UpdatedBy = articleInfo.UpdatedBy;
            article.ModuleId = articleInfo.ModuleId;

            return article;
        }
    }
}
