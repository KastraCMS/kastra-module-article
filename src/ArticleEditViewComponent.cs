using System;
using System.Threading.Tasks;
using Kastra.Core.Attributes;
using Kastra.Core.Dto;
using Kastra.Core.ViewComponents;
using Kastra.Module.Article.Business.Contracts;
using Kastra.Module.Article.DAL;
using Kastra.Module.Article.DTO;
using Kastra.Module.Article.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Kastra.Module.Article
{
    public class ArticleEditViewComponent : ModuleViewComponent
    {
        [ParameterAttribute("ArticleId")]
        public Int32 ArticleId { get; set; }
        
        private readonly ArticleContext _dbContext = null;
        private readonly IArticleBusiness _articleBusiness = null;
        private readonly UserManager<ApplicationUser> _userManager;

        public ArticleEditViewComponent(ArticleContext dbContext, UserManager<ApplicationUser> userManager, IArticleBusiness articleBusiness)
        {
            _dbContext = dbContext;
            _articleBusiness = articleBusiness;
            _userManager = userManager;
        }

        public override Task<ViewViewComponentResult> OnViewComponentLoad()
        {
            AdminArticleModel model = new AdminArticleModel(this);
            ArticleInfo article = null;

            // Update
            if(model.ValidForm)
            {
                String userId = _userManager.GetUserId(HttpContext.User);
                article = _articleBusiness.GetArticle(ArticleId);

                if(article == null)
                {
                    article = new ArticleInfo();
                    article.CreatedAt = DateTime.UtcNow;
                    article.CreatedBy = userId;
                }
                    
                article.ArticleId = model.ArticleId;
                article.ArticleContent = model.ArticleContent;
                article.ArticleOrder = model.ArticleOrder;
                article.CreatedAt = DateTime.Now;
                article.ImageUrl = model.ImageUrl;
                article.Title = model.Title;
                article.UserId = userId;
                article.UpdatedAt = DateTime.UtcNow;
                article.UpdatedBy = userId;
                article.ModuleId = Module.ModuleId;
                
                _articleBusiness.SaveArticle(article);
            }

            if(article == null)
            {
                article = _articleBusiness.GetArticle(ArticleId);
            }

            if(article == null)
            {
                return Task.FromResult(ModuleView("ArticleEdit", model));
            }

            model.ArticleId = ArticleId;
            model.ArticleContent = article.ArticleContent;
            model.ArticleOrder = article.ArticleOrder;
            model.ImageUrl = article.ImageUrl;
            model.Title = article.Title;

            return Task.FromResult(ModuleView("ArticleEdit", model));
        }
    }
}