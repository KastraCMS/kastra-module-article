namespace Kastra.Module.Article.Models.API
{
    public class ArticleModel
    {
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public string ArticleContent { get; set; }

        public string ImageUrl { get; set; }

        public int ArticleOrder { get; set; }

        public int ModuleId { get; set; }

        public string DateUpdated { get; set; }
    }
}