using BLL.Data.Service;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IService_Article articleManager;

        public ArticleController(IService_Article articleManager)
        {
            this.articleManager = articleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var articleQuery = await Task.Run(() => articleManager.Gets(new Article() { 
                Id = id
            },"Articles","Id","desc",0,1));
            var article = articleQuery.Data.FirstOrDefault();
            return View(article);
        }
    }
}
