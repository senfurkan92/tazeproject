using BLL.Data.Service;
using Domain;
using Microsoft.AspNetCore.Cors;
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
        public CategoryApiController(IService_Category categoryManager, UserManager<AppUser> userManager) : base (categoryManager, userManager)
        {

        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] object data)
        {
            var dataJson = (JObject)JsonConvert.DeserializeObject(data.ToString());
            var category = new Category()
            {
                AppUserId = await GetUserId(),
                Name = dataJson["categoryName"].ToString(),
            };
            var result = await Task.Run(() => categoryManager.Insert(category, "Categories"));
            return Ok(GetJsonResult(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Task.Run(() => categoryManager.Gets(new Category() { 
                IsActive = true,
                IsDeleted = false,
            },"Categories","Id","asc",0,-1));
            return Ok(GetJsonResult(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Task.Run(() => categoryManager.UpdateById(new Category() { 
                Id = id,
                DeleteDate = DateTime.Now,
                IsDeleted = true,
                IsActive = false
            },"Categories"));
            return Ok(GetJsonResult(result));
        }
    }
}
