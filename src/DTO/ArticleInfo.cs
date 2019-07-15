using System;
namespace Kastra.Module.Article.DTO
{
    public class ArticleInfo
    {
			public int ArticleId { get; set; }
			public string Title { get; set; }
			public string ArticleContent { get; set; }
			public DateTime CreatedAt { get; set; }
			public DateTime UpdatedAt { get; set; }
			public string CreatedBy { get; set; }
			public string UpdatedBy { get; set; }
			public string UserId { get; set; }
			public string ImageUrl { get; set; }
			public int ArticleOrder { get; set; }
			public int ModuleId { get; set; }
			public string AuthorName { get; set; }
    }
}
