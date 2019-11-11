using System.Linq;
using System.Threading.Tasks;
using Kastra.Core.Attributes;
using Kastra.Core.Modules;
using Kastra.Core.Modules.ViewComponents;
using Kastra.Module.Article.Business.Contracts;
using Kastra.Module.Article.DAL;
using Kastra.Module.Article.Models;

namespace Kastra.Module.Article
{
    [ViewComponentAuthorize(Claims = "GlobalSettingsEdition")]
    public class SettingsViewComponent : ModuleViewComponent
    {
        [Parameter("ArticleId")]
        public int ArticleId { get; set; }

        private readonly ArticleContext _dbContext;
        private readonly IArticleBusiness _articleBusiness;

        public SettingsViewComponent(ArticleContext dbContext, IArticleBusiness articleBusiness)
        {
            _dbContext = dbContext;
            _articleBusiness = articleBusiness;
        }
        
        public override Task<ModuleViewComponentResult> OnViewComponentLoad()
        {
            ArticlesModel model = new ArticlesModel(this);
            model.Articles = _articleBusiness.GetArticlesList(Module.ModuleId).OrderByDescending(a => a.ArticleOrder).ToList();
            model.PageId = Page.PageId;

            return Task.FromResult(ModuleView("Settings", model));
        }

        public override Task<ModuleViewComponentResult> OnViewComponentUpdate()
        {
            ArticlesModel model = new ArticlesModel(this);
            model.Articles = _articleBusiness.GetArticlesList(Module.ModuleId).OrderByDescending(a => a.ArticleOrder).ToList();
            model.PageId = Page.PageId;

            return Task.FromResult(ModuleView("Settings", model));
        }

        public override Task<ModuleViewComponentResult> OnViewComponentDelete()
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