using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Controllers
{
    public class CategoryController : Controller
    {
        /// <summary>
        /// Tum kategorilerin sergilenmesi
        /// Kategori yonetimi (ekleme ve silme)
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
