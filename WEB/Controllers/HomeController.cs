using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OpenQA.Selenium.Interactions;
using Selenium;
using System.Web;

namespace WEB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Proje hakkinda genel bilgi
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {          
            return View();
        }
    }
}
