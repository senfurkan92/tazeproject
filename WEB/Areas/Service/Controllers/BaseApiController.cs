using BLL.Data.Service;
using Domain;
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
        protected readonly UserManager<AppUser> userManager;
        private readonly JsonSerializerSettings serializerSettings;

        public BaseApiController(IService_Category categoryManager, UserManager<AppUser> userManager)
        {
            this.categoryManager = categoryManager;
            this.userManager = userManager;
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
