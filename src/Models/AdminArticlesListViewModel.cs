using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kastra.Core.ViewComponents;

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

        public Int32 ArticleId { get; set; }

        [Display(Name="Title :")]
        public String Title { get; set; }

        [Display(Name="Content:")]
        public String ArticleContent { get; set; }

        [Display(Name="Image Url :")]
        public String ImageUrl { get; set; }

        [Display(Name="Order :")]
        public Int32 ArticleOrder { get; set; }
    }
}