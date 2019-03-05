using System;
using System.Collections.Generic;
using System.Linq;
using Kastra.Core.Business;
using Kastra.Core.Controllers;
using Kastra.Core.Dto;
using Kastra.Module.Article.Business.Contracts;
using Kastra.Module.Article.DTO;
using Kastra.Module.Article.Models.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kastra.Module.Article.Controllers
{
    [Authorize("Administration")]
    public class ArticleController : ModuleController
    {
        private IArticleBusiness _articleBusiness = null;
        private readonly UserManager<ApplicationUser> _userManager = null;

        public ArticleController(IViewManager viewManager, IArticleBusiness articleBusiness, UserManager<ApplicationUser> userManager) : base(viewManager)
        {
            _articleBusiness = articleBusiness;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            List<ArticleModel> model = _articleBusiness
                        .GetArticlesList(id)
                        .OrderByDescending(a => a.ArticleOrder)
                        .Select(a => new ArticleModel() {
                            ArticleId = a.ArticleId,
                            ArticleContent = a.ArticleContent,
                            ArticleOrder = a.ArticleOrder,
                            ImageUrl = a.ImageUrl,
                            Title = a.Title,
                            DateUpdated = a.UpdatedAt.ToString("dd/MM/yyyy HH:mm")
                        }).OrderByDescending(a => a.ArticleOrder)
                            .ThenByDescending(a => a.DateUpdated)
                            .ToList();
            
            return Json(model);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            ArticleInfo article = _articleBusiness.GetArticle(id);

            if (article == null)
            {
                return NotFound();
            }

            ArticleModel model = new ArticleModel();
            model.ArticleContent = article.ArticleContent;
            model.ArticleId = article.ArticleId;
            model.ArticleOrder = article.ArticleOrder;
            model.ImageUrl = article.ImageUrl;
            model.ModuleId = article.ModuleId;
            model.Title = article.Title;
            model.DateUpdated = article.UpdatedAt.ToString("dd/MM/yyyy");

            return Json(model);
        }

        [HttpPost]
        public IActionResult Update([FromBody] ArticleModel model)
        {
            ArticleInfo article = _articleBusiness.GetArticle(model.ArticleId);
            String userId = _userManager.GetUserId(User);

            if(article == null)
            {
                article = new ArticleInfo();
                article.CreatedAt = DateTime.Now;
                article.CreatedBy = userId;
            }
                
            article.ArticleId = model.ArticleId;
            article.ArticleContent = model.ArticleContent;
            article.ArticleOrder = model.ArticleOrder;
            article.CreatedAt = DateTime.Now;
            article.ImageUrl = model.ImageUrl;
            article.Title = model.Title;
            article.UserId = userId;
            article.UpdatedAt = DateTime.Now;
            article.UpdatedBy = userId;
            article.ModuleId = model.ModuleId;
            
            _articleBusiness.SaveArticle(article);

            return Ok(new { article.ArticleId });
        }

        [HttpDelete]
        public IActionResult Delete([FromBody]int id)
        {
            _articleBusiness.DeleteArticle(id);

            return Ok();
        }
    }
}