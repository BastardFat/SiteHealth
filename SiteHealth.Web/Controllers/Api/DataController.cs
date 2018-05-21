using SiteHealth.Controllers.Base;
using SiteHealth.Services.Interfaces;
using SiteHealth.Services.ViewModels.Endpoint;
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
        private readonly IReportingService _reportingService;
        public DataController(
            IEndpointService endpointService,
            IReportingService reportingService)
        {
            _endpointService = endpointService;
            _reportingService = reportingService;
        }

        [HttpGet]
        [Route("sites/get")]
        public async Task<PagedDataSource<SiteViewModel>> GetSites(int page, string search)
        {
            return await _endpointService.GetSites(page, search);
        }

        [HttpGet]
        [Route("sites/get")]
        public async Task<SiteViewModelWithDetailedChilds> GetSite(long id)
        {
            var result = await _endpointService.GetSite(id);
            foreach (var ep in result.Endpoints)
            {
                var detailed = (ep as EndpointViewModelWithDetails);
                detailed.Uptime = await _reportingService.CalculateUptime(ep.Id);
                detailed.Chart = await _reportingService.GetChartForEndpointLastHours(ep.Id);
                detailed.Stat = await _reportingService.GetStatistic(ep.Id);
            }
            return result;
        }
    }
}