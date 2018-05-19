using SiteHealth.Controllers.Base;
using SiteHealth.Services.Interfaces;
using SiteHealth.Services.ViewModels.Endpoint;
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
        [Route("site/save")]
        public async Task<SiteViewModel> SaveSite(SiteViewModel model)
        {
            return await _configurationService.SaveSite(model);
        }

        [HttpDelete]
        [Route("site/remove")]
        public async Task RemoveSite(long id)
        {
            await _configurationService.RemoveSite(id);
        }

        [HttpPost]
        [Route("enpoint/save")]
        public async Task<EndpointViewModel> SaveEndpoint(EndpointViewModel model)
        {
            return await _configurationService.SaveEndpoint(model);
        }

        [HttpDelete]
        [Route("enpoint/remove")]
        public async Task SaveEndpoint(long id)
        {
            await _configurationService.RemoveEndpoint(id);
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