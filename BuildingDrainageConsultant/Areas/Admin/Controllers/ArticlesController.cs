namespace BuildingDrainageConsultant.Areas.Admin.Controllers
{
    using AutoMapper;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Models.Articles;
    using BuildingDrainageConsultant.Services.Articles;
    using BuildingDrainageConsultant.Services.Articles.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class ArticlesController : AdminController
    {
        private readonly IArticleService articles;
        private readonly IMapper mapper;

        public ArticlesController(
            IArticleService articles,
            IMapper mapper)
        {
            this.articles = articles;
            this.mapper = mapper;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(ArticleFormModel article)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
            }

            this.articles.Create(
                article.Title,
                article.Content,
                article.ImageUrl);

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
                article.ImageUrl);

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
    }
}
