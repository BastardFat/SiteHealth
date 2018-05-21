using Hangfire;
using Hangfire.Storage;
using SiteHealth.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SiteHealth.Web.Background
{
    public static class BackgroundJobManager
    {
        private static string GetStringId(long id) => $"JOB_{id}";

        public static async Task UpdateWorker(long siteId, IConfigurationService configurationService)
        {
            var site = await configurationService.GetSite(siteId);
            var param = new BackgroundJobInfo(siteId, site.FetchIntervalInMinutes, site.Endpoints);
            RecurringJob.AddOrUpdate(GetStringId(siteId), () => MakeReports(param), Cron.MinuteInterval(site.FetchIntervalInMinutes));
        }

        public static void RemoveWorker(long siteId)
        {
            RecurringJob.RemoveIfExists(GetStringId(siteId));
        }

        public static void MakeReports(BackgroundJobInfo jobInfo)
        {
            var resolver = DependencyResolverConfig.GetResolver(true);
            var service = (IReportingService) resolver.GetService(typeof(IReportingService));

            var fetcher = new EndpointFetcher();
            foreach (var endpoint in jobInfo.Endpoints)
            {
                var report = fetcher.MakeHealthReport(endpoint.Url, endpoint.Id);
                report.IntervalInMinutes = jobInfo.FetchIntervalInMinutes;
                service.AddReportSyncroniously(report);
            }

        }

    }
}