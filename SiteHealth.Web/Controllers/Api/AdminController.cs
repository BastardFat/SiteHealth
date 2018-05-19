using SiteHealth.Controllers.Base;
using SiteHealth.Services.Interfaces;
using SiteHealth.Services.ViewModels;
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
    }
}