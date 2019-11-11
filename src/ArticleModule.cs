using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Kastra.Core.Business;
using Kastra.Core.Modules;
using Kastra.Module.Article.Business;
using Kastra.Module.Article.Business.Contracts;
using Kastra.Module.Article.DAL;

namespace Kastra.Module.Article
{
    public class ArticleModule : ModuleBase
    {
        public override void SetDependencyInjections(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ArticleContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IArticleBusiness, ArticleBusiness>();
        }

        public override void Install(IServiceProvider serviceProvider, IViewManager viewManager)
        {
            base.Install(serviceProvider, viewManager);

            ArticleContext dbContext = serviceProvider.GetService<ArticleContext>();

            if(dbContext == null)
                throw new Exception("Unable to install Article tables");

            dbContext.Database.Migrate();
        }
    }
}
