using SiteHealth.Entity.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;

namespace SiteHealth.Web.Background
{
    public class EndpointFetcher
    {
        public HealthReport MakeHealthReport(string url, long endpointId)
        {
            var report = new HealthReport
            {
                Timestamp = DateTime.UtcNow,
                EndpointId = endpointId,
                Id = 0
            };

            var sw = new Stopwatch();


            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                sw.Start();
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                sw.Stop();
                report.StatusCode = (int)resp.StatusCode;
            }
            catch (Exception ex)
            {
                report.Error = ex.Message;
            }


            report.ResponseTime = sw.ElapsedMilliseconds;

            return report;
        }
    }
}