using AutoMapper;
using BLL.Data.Service;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Areas.Service.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly IService_Category categoryManager;
        protected readonly IService_Article articleManager;
        protected readonly UserManager<AppUser> userManager;
        protected readonly JsonSerializerSettings serializerSettings;
        protected readonly IWebHostEnvironment env;
        protected readonly IMapper mapper;

        public BaseApiController(IService_Category categoryManager, IService_Article articleManager, UserManager<AppUser> userManager, IWebHostEnvironment env, IMapper mapper)
        {
            this.categoryManager = categoryManager;
            this.articleManager = articleManager;
            this.userManager = userManager;
            this.env = env;
            this.mapper = mapper;
            this.serializerSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public async Task<string> GetUserId()
        { 
            return (await Task.Run(() => userManager.FindByNameAsync(User.Identity.Name))).Id;
        }

        public string GetJsonResult(object result)
        {
            return JsonConvert.SerializeObject(result, serializerSettings);
        }
    }
}
