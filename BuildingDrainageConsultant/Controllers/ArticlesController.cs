namespace BuildingDrainageConsultant.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.Articles;
    using BuildingDrainageConsultant.Services.Articles;
    using BuildingDrainageConsultant.Services.Articles.Models;
    using BuildingDrainageConsultant.Services.Images;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    using static Areas.Admin.AdminConstants;

    [Authorize(Roles = AdministratorRoleName)]
    public class ArticlesController : Controller
    {
        private readonly IArticleService articles;
        private readonly IImageHLService images;
        private readonly IMapper mapper;

        public ArticlesController(
            IArticleService articles,
            IImageHLService images,
            IMapper mapper)
        {
            this.articles = articles;
            this.images = images;
            this.mapper = mapper;
        }

        public IActionResult Add(ArticleFormModel article) => View(article);

        [HttpPost]
        public IActionResult Add(ArticleFormModel article, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
            }

            this.articles.Create(
                article.Title,
                article.Content,
                article.ImageId);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All(IEnumerable<ArticleServiceModel> query)
        {
            query = this.articles.All();

            return View(query);
        }

        public IActionResult Edit(int id)
        {
            var article = this.articles.Details(id);

            var articleForm = this.mapper.Map<ArticleFormModel>(article);

            return View(articleForm);
        }

        [HttpPost]
        public IActionResult Edit(int id, ArticleFormModel article)
        {

            if (!ModelState.IsValid)
            {
                return View(article);
            }

            if (!User.IsAdmin())
            {
                return BadRequest();
            }

            this.articles.Edit(
                id,
                article.Title,
                article.Content,
                article.ImageId);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var article = this.articles.Delete(id);

            if (article == false)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(All));
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var drain = this.articles.Details(id);

            if (drain == null)
            {
                return NotFound();
            }

            var drainForm = this.mapper.Map<ArticleFormModel>(drain);

            return View(drainForm);
        }

        public IActionResult AddImage(ArticleFormModel articleCreateInfo)
        {
            articleCreateInfo.Images = this.images.GetArticlesImages();

            return View(articleCreateInfo);
        }

        [HttpPost]
        public IActionResult AddImage(int id, ArticleFormModel articleCreateInfo)
        {
            var drainImage = this.images.GetImageById(id);

            if (drainImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "AddImage", articleCreateInfo) });
            }

            articleCreateInfo.Image = drainImage;
            articleCreateInfo.ImageId = id;

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Add", articleCreateInfo) });
        }

        public IActionResult EditImage(int modelId, ArticleFormModel articleCreateInfo)
        {
            articleCreateInfo.Images = this.images.GetArticlesImages();
            articleCreateInfo.Id = modelId;

            return View(articleCreateInfo);
        }

        [HttpPost]
        public IActionResult EditImage(int id, int modelId, ArticleFormModel articleCreateInfo)
        {
            var drain = this.articles.Details(modelId);
            articleCreateInfo = this.mapper.Map<ArticleFormModel>(drain);

            var drainImage = this.images.GetImageById(id);
            articleCreateInfo.Image = drainImage;
            articleCreateInfo.ImageId = id;

            if (drainImage == null)
            {
                return Json(new { isValid = false, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "EditImage", articleCreateInfo) });
            }

            return Json(new { isValid = true, html = AjaxRenderHtmlHelper.RenderRazorViewToString(this, "Edit", articleCreateInfo) });
        }
    }
}
