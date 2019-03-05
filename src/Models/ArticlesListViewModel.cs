using System;
using System.Collections.Generic;
using Kastra.Core.ViewComponents;
using Kastra.Module.Article.DTO;

namespace Kastra.Module.Article.Models
{
    public class ArticleModel: ModuleModelBinder
    {
        public ArticleModel(ModuleViewComponent moduleView) : base(moduleView)
        {
        }

        public IList<ArticleInfo> Articles { get; set; }

        public Int32 PageId { get; set; }
    }
}