using SiteHealth.Entity.Models;
using SiteHealth.Services.ViewModels.HealthReport;
using SiteHealth.Services.ViewModels.Site;
using SiteHealth.Services.ViewModels.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteHealth.Services.Interfaces
{
    public interface IReportingService
    {
        Task<ChartItem[]> GetChartForEndpointLastHours(long endpointId, int hours = 10);
        Task<double?> CalculateUptime(long endpointId, int days = 30);
        Task<StatusesAndErrors> GetStatistic(long endpointId, int days = 30);
        Task AddReport(HealthReport report);
        void AddReportSyncroniously(HealthReport report);
    }
}
