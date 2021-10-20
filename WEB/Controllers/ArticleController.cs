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

        /// <summary>
        /// Tum yazilar
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Yeni yazi ekleme
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Yazı okuma
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
