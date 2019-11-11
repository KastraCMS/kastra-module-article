using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kastra.Core.Modules;
using Kastra.Core.Modules.ViewComponents;

namespace Kastra.Module.Article.Models
{
    public class AdminArticlesModel : ModuleModelBinder
    {
        public AdminArticlesModel(ModuleViewComponent moduleView) : base(moduleView)
        {
        }

        public List<AdminArticleModel> Articles { get; set; }
    }

    public class AdminArticleModel : ModuleModelBinder
    {
        public AdminArticleModel(ModuleViewComponent moduleView) : base(moduleView)
        {
        }

        public int ArticleId { get; set; }

        [Display(Name="Title :")]
        public string Title { get; set; }

        [Display(Name="Content:")]
        public string ArticleContent { get; set; }

        [Display(Name="Image Url :")]
        public string ImageUrl { get; set; }

        [Display(Name="Order :")]
        public int ArticleOrder { get; set; }
    }
}