using System.Collections.Generic;
using System.Linq;
using Kastra.Core.ViewComponents;
using Kastra.Module.Article.Business.Contracts;
using Kastra.Module.Article.DAL;
using Kastra.Module.Article.DTO;
using Kastra.Module.Article.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Kastra.Module.Article
{
    [ViewComponent(Name = "Kastra.Module.Article.Article")]
    public class ArticleViewComponent : ModuleViewComponent
    {
        private readonly ArticleContext _dbContext = null;
        private readonly IArticleBusiness _articleManager = null;

        public ArticleViewComponent(ArticleContext dbContext, IArticleBusiness articleBusiness)
        {
            _dbContext = dbContext;
            _articleManager = articleBusiness;
        }
        
        public override ViewViewComponentResult OnViewComponentLoad()
        {
            ArticleModel model = new ArticleModel(this);
            model.Articles = _articleManager.GetArticlesList(Module.ModuleId)
                                ?.OrderByDescending(a => a.ArticleOrder)
                                ?.ThenByDescending(a => a.UpdatedAt)
                                ?.ToList() ?? new List<ArticleInfo>();

            return ModuleView("Index", model);
        }
    }
}