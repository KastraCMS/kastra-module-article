using Microsoft.EntityFrameworkCore;

namespace Kastra.Module.Article.DAL
{
    public class ArticleContext : DbContext
    {
        public virtual DbSet<KastraArticles> KastraArticles { get; set; }

		public ArticleContext(DbContextOptions<ArticleContext> options)
            : base(options)
        {
			
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<KastraArticles>(entity =>
			{
				entity.HasKey(e => e.ArticleId)
					.HasName("PK_Kastra_Articles");

				entity.ToTable("Kastra_Articles");

				entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

				entity.Property(e => e.ImageUrl).HasMaxLength(1000);

				entity.Property(e => e.Title).HasMaxLength(250);

				entity.Property(e => e.UserId)
					.IsRequired()
					.HasColumnName("UserID")
					.HasMaxLength(450);

				entity.Property(e => e.CreatedAt)
					.HasColumnType("datetime")
					.HasDefaultValueSql("getdate()");

				entity.Property(e => e.UpdatedAt)
					.HasColumnType("datetime")
					.HasDefaultValueSql("getdate()");

				entity.Property(e => e.UpdatedBy)
					.HasColumnName("UpdatedBy")
					.HasMaxLength(450);

				entity.Property(e => e.CreatedBy)
					.HasColumnName("CreatedBy")
					.HasMaxLength(450);

				entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
			});
        }


    }
}
