using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kastra.Core.Attributes;
using Kastra.Core.Modules;
using Kastra.Core.Modules.ViewComponents;
using Kastra.Module.Article.Business.Contracts;
using Kastra.Module.Article.DAL;
using Kastra.Module.Article.DTO;
using Kastra.Module.Article.Models;
using Microsoft.AspNetCore.Mvc;
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

        private readonly ArticleContext _dbContext;
        private readonly IArticleBusiness _articleManager;

        public ArticleViewComponent(ArticleContext dbContext, IArticleBusiness articleBusiness)
        {
            _dbContext = dbContext;
            _articleManager = articleBusiness;
        }
        
        public override Task<ModuleViewComponentResult> OnViewComponentLoad()
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

            return Task.FromResult(ModuleView("Index", model));
        }
    }
}