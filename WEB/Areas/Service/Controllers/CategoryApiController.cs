﻿using AutoMapper;
using BLL.Data.Service;
using Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Areas.Service.Controllers
{
    [EnableCors("FullAllow")]
    [ApiController]
    [Route("Service/[controller]/[action]")]
    public class CategoryApiController : BaseApiController
    {
        public CategoryApiController(IService_Category categoryManager, IService_Article articleManager, UserManager<AppUser> userManager, IWebHostEnvironment env, IMapper mapper) 
            : base (categoryManager, articleManager, userManager, env, mapper)
        {

        }

        /// <summary>
        /// axios ile yeni kategori ekleyebilmek icin restapi
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] object data)
        {
            var dataJson = (JObject)JsonConvert.DeserializeObject(data.ToString());
            var category = new Category()
            {
                AppUserId = await GetUserId(),
                Name = dataJson?["categoryName"]?.ToString(),
            };
            var result = await Task.Run(() => categoryManager.Insert(category, "Categories"));           
            return Ok(GetJsonResult(result));
        }

        /// <summary>
        /// axios ile kategorileri getirmek icin restapi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Task.Run(() => categoryManager.Gets(new Category() { 
                IsActive = true,
                IsDeleted = false,
            },"Categories","Id","asc",0,-1));
            return Ok(GetJsonResult(result));
        }

        /// <summary>
        /// axios ile yazı silmek icin restapi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Task.Run(() => categoryManager.UpdateById(new Category() { 
                Id = id,
                DeleteDate = DateTime.Now,
                IsDeleted = true,
                IsActive = false
            },"Categories"));

            if (result.Success)
            {
                var relatedArticles = await Task.Run(()=> articleManager.Gets(new Article()
                {
                    CategoryId = id,
                    IsDeleted = false
                },"Articles","Id","asc",0,-1));
                if (relatedArticles.Success)
                {
                    foreach(var article in relatedArticles.Data.ToList())
                    {
                        await Task.Run(()=> articleManager.UpdateById(new Article()
                        {
                            Id = article.Id,
                            DeleteDate = DateTime.Now,
                            IsDeleted = true,
                            IsActive = false
                        }, "Articles"));
                    };
                }
            }

            return Ok(GetJsonResult(result));
        }
    }
}
