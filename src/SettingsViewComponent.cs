using System;
using System.Linq;
using Kastra.Core.Attributes;
using Kastra.Core.ViewComponents;
using Kastra.Module.Article.Business.Contracts;
using Kastra.Module.Article.DAL;
using Kastra.Module.Article.Models;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Kastra.Module.Article
{
    [ViewComponentAuthorize(Claims = "GlobalSettingsEdition")]
    public class SettingsViewComponent : ModuleViewComponent
    {
        [ParameterAttribute("ArticleId")]
        public Int32 ArticleId { get; set; }

        private readonly ArticleContext _dbContext = null;
        private readonly IArticleBusiness _articleBusiness = null;

        public SettingsViewComponent(ArticleContext dbContext, IArticleBusiness articleBusiness)
        {
            _dbContext = dbContext;
            _articleBusiness = articleBusiness;
        }
        
        public override ViewViewComponentResult OnViewComponentLoad()
        {
            ArticlesModel model = new ArticlesModel(this);
            model.Articles = _articleBusiness.GetArticlesList(Module.ModuleId).OrderByDescending(a => a.ArticleOrder).ToList();
            model.PageId = Page.PageId;

            return ModuleView("Settings", model);
        }

        public override ViewViewComponentResult OnViewComponentUpdate()
        {
            ArticlesModel model = new ArticlesModel(this);
            model.Articles = _articleBusiness.GetArticlesList(Module.ModuleId).OrderByDescending(a => a.ArticleOrder).ToList();
            model.PageId = Page.PageId;

            return ModuleView("Settings", model);
        }

        public override ViewViewComponentResult OnViewComponentDelete()
        {
            if(ArticleId <= 0)
                return OnViewComponentLoad();

            // Delete article
            _articleBusiness.DeleteArticle(ArticleId);
            
            ViewData["Message"] = "Article was deleted";

            return OnViewComponentLoad();
        }
    }
}