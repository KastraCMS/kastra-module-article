using System.Collections.Generic;
using System.Linq;
using Kastra.Core.Attributes;
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
        #region Parameters

        /// <summary>
        /// Page index contained in the query string.
        /// </summary>
        /// <value></value>
        [Parameter("pageindex")]
        public int PageIndex { get; set; }

        #endregion

        private readonly ArticleContext _dbContext = null;
        private readonly IArticleBusiness _articleManager = null;

        public ArticleViewComponent(ArticleContext dbContext, IArticleBusiness articleBusiness)
        {
            _dbContext = dbContext;
            _articleManager = articleBusiness;
        }
        
        public override ViewViewComponentResult OnViewComponentLoad()
        {
            ArticlesModel model = new ArticlesModel(this);
            model.PageName = Page.KeyName;
            model.PageIndex = PageIndex;

            // Pagination
            model.PageSize = model.PageSize == 0 ? 5 : model.PageSize;

            int skip = model.PageIndex * model.PageSize;
            
            model.PageTotal = _articleManager.CountArticles(Module.ModuleId);
            model.Articles = _articleManager.GetArticlesList(Module.ModuleId, skip, model.PageSize)
                                ?.OrderByDescending(a => a.ArticleOrder)
                                ?.ThenByDescending(a => a.UpdatedAt)
                                ?.ToList() ?? new List<ArticleInfo>();

            return ModuleView("Index", model);
        }
    }
}