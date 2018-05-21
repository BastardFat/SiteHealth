using SiteHealth.Services.ViewModels.Endpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteHealth.Web.Background
{
    public class BackgroundJobInfo
    {
        public BackgroundJobInfo(long siteId, int fetchIntervalInMinutes, IEnumerable<EndpointViewModel> endpoints)
        {
            FetchIntervalInMinutes = fetchIntervalInMinutes;
            Endpoints = endpoints.ToArray();
            SiteId = siteId;
        }

        public int FetchIntervalInMinutes { get; set; }
        public long SiteId { get; set; }
        public EndpointViewModel[] Endpoints { get; set; }

    }
}