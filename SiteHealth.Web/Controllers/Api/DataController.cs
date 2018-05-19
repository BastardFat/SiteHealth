using SiteHealth.Controllers.Base;
using SiteHealth.Services.Interfaces;
using SiteHealth.Services.ViewModels.Site;
using SiteHealth.Services.ViewModels.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SiteHealth.Web.Controllers.Api
{
    [RoutePrefix("api/data")]
    public class DataController : BaseApiController
    {
        private readonly IEndpointService _endpointService;
        public DataController(IEndpointService endpointService)
        {
            _endpointService = endpointService;
        }

        [HttpGet]
        [Route("sites/get")]
        public async Task<PagedDataSource<SiteViewModel>> GetSites(int page, string search)
        {
            return await _endpointService.GetSites(page, search);
        }
    }
}