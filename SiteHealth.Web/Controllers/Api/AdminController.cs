using Hangfire;
using SiteHealth.Controllers.Base;
using SiteHealth.Services.Interfaces;
using SiteHealth.Services.ViewModels.Endpoint;
using SiteHealth.Services.ViewModels.Site;
using SiteHealth.Web.Background;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SiteHealth.Web.Controllers.Api
{
    [RoutePrefix("api/admin")]
    public class AdminController : BaseApiController
    {
        private readonly IConfigurationService _configurationService;
        public AdminController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }


        [HttpPost]
        [Route("site/save")]
        public async Task<SiteViewModelWithChilds> SaveSite(SiteViewModelWithChilds model)
        {
            var res = await _configurationService.SaveSite(model);
            await BackgroundJobManager.UpdateWorker(res.Id, _configurationService);
            return res;
        }

        [HttpDelete]
        [Route("site/remove")]
        public async Task RemoveSite(long id)
        {
            await _configurationService.RemoveSite(id);
            BackgroundJobManager.RemoveWorker(id);
        }

        [HttpGet]
        [Route("site/edit")]
        public async Task<SiteViewModelWithChilds> EditSite(long id)
        {
            return await _configurationService.GetSite(id);
        }

        [HttpPost]
        [Route("options/set")]
        public async Task SetOption([FromUri] string key, [FromUri] string type, [FromBody] object model)
        {
            await _configurationService.SetOption(key, model, type);
        }

        [HttpGet]
        [Route("options/get")]
        public async Task<Dictionary<string, object>> GetOptions()
        {
            return await _configurationService.GetOptions();
        }

        [HttpGet]
        [Route("options/get")]
        public async Task<object> GetOption(string key)
        {
            return await _configurationService.GetOption(key);
        }
    }
}