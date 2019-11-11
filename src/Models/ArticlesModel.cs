using System.Collections.Generic;
using Kastra.Core.Modules;
using Kastra.Core.Modules.ViewComponents;
using Kastra.Module.Article.DTO;

namespace Kastra.Module.Article.Models
{
    public class ArticlesModel: ModuleModelBinder
    {
        public ArticlesModel(ModuleViewComponent moduleView) : base(moduleView)
        {
        }

        public IList<ArticleInfo> Articles { get; set; }

        public int PageId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int PageTotal { get; set; }

        public string PageName { get; set; }
    }
}