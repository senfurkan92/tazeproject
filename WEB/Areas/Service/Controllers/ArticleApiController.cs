using AutoMapper;
using BLL.Data.Service;
using CORE.Prevail.Model;
using Domain;
using DTO.ArticleDtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Areas.Service.Controllers
{
    [EnableCors("FullAllow")]
    [ApiController]
    [Route("Service/[controller]/[action]")]
    public class ArticleApiController : BaseApiController
    {
        public ArticleApiController(IService_Article articleManager, IService_Category categoryManager, UserManager<AppUser> userManager, IWebHostEnvironment env, IMapper mapper)
            : base(categoryManager, articleManager, userManager, env, mapper)
        {

        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] ArticleInsertDto dto)
        {
            ResultModel<Article> result = null;
            var article = mapper.Map<ArticleInsertDto, Article>(dto);
            var path = Path.Combine(env.WebRootPath, "uploads", dto.ImgPathName);
            FileStream fs = new FileStream(path, FileMode.Create);
            try
            {           
                await dto.ImgFile.CopyToAsync(fs);
                article.ImgPath = "/uploads/" + dto.ImgPathName;
                article.AppUserId = await GetUserId();
                var query = await Task.Run(() => articleManager.Insert(article, "Articles"));
                result = query;
            }
            catch (Exception ex)
            {
                result = new ResultModel<Article>(false, ex.GetInnestException().Message, null);
            }
            finally 
            {
                fs.Close();    
            }
            return Ok(GetJsonResult(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoryQuery = await Task.Run(() => categoryManager.Gets(new Category(), "Categories", "Id", "asc",0,-1));
            var categories = categoryQuery.Data.ToList();
            var articleQuery = await Task.Run(() => articleManager.Gets(new Article() { 
                IsActive = true,
                IsDeleted = false,
            }, "Articles", "Id", "asc", 0, -1));
            var articles = articleQuery.Data.ToList();
            var presentList = new List<ArticlePresentDto>();
            articles.ForEach(x =>
            {
                var present = mapper.Map<Article, ArticlePresentDto>(x);
                present.CategoryName = categories?.FirstOrDefault(y => y.Id == x.CategoryId)?.Name;
                presentList.Add(present);        
            });
            var result = new ResultModel<List<ArticlePresentDto>>(true, "", presentList);
            return Ok(GetJsonResult(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Task.Run(() => articleManager.UpdateById(new Article()
            {
                Id = id,
                DeleteDate = DateTime.Now,
                IsDeleted = true,
                IsActive = false
            }, "Articles"));
            return Ok(GetJsonResult(result));
        }
    }
}
