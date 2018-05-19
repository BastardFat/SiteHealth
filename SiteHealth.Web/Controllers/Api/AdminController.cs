using SiteHealth.Controllers.Base;
using SiteHealth.Services.Interfaces;
using SiteHealth.Services.ViewModels.Site;
using System;
using System.Collections.Generic;
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
        [Route("site/edit")]
        public async Task<SiteViewModel> AddOrUpdateSite(SiteViewModel model)
        {
            return await _configurationService.AddOrUpdateSite(model);
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