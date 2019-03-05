using System;
namespace Kastra.Module.Article.DTO
{
    public class ArticleInfo
    {
			public Int32 ArticleId { get; set; }
			public String Title { get; set; }
			public String ArticleContent { get; set; }
			public DateTime CreatedAt { get; set; }
			public DateTime UpdatedAt { get; set; }
			public String CreatedBy { get; set; }
			public String UpdatedBy { get; set; }
			public String UserId { get; set; }
			public String ImageUrl { get; set; }
			public Int32 ArticleOrder { get; set; }
			public Int32 ModuleId { get; set; }
    }
}
