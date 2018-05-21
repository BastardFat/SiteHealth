using AutoMapper;
using AutoMapper.QueryableExtensions;
using SiteHealth.Database.Concrete.Interfaces;
using SiteHealth.Database.Repos.Interfaces;
using SiteHealth.Entity.Models;
using SiteHealth.Services.Implementations.Base;
using SiteHealth.Services.Interfaces;
using SiteHealth.Services.ViewModels.HealthReport;
using SiteHealth.Services.ViewModels.Site;
using SiteHealth.Services.ViewModels.Utils;
using SiteHealth.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Services.Implementations
{
    [NinjectDependency(typeof(IReportingService), NinjectDependencyScope.Request)]
    public class ReportingService : BaseService, IReportingService
    {
        private readonly IHealthReportRepository _healthReportRepository;
        private readonly IEndpointRepository _endpointRepository;
        private readonly ISiteHealthUnitOfWork _uow;

        private readonly IMapper _simpleMapper;

        public ReportingService(
            IHealthReportRepository healthReportRepository,
            IEndpointRepository endpointRepository,
            ISiteHealthUnitOfWork uow)
        {
            _healthReportRepository = healthReportRepository;
            _endpointRepository = endpointRepository;
            _uow = uow;

            _simpleMapper = CreateSimpleMapper();
        }


        public async Task<ChartItem[]> GetChartForEndpointLastHours(long endpointId, int hours = 10)
        {
            var now = DateTime.UtcNow;
            return await _healthReportRepository
                .Query()
                .Where(x => x.EndpointId == endpointId && DbFunctions.DiffHours(x.Timestamp, now) < hours && x.ResponseTime != null)
                .Select(x => new ChartItem { Time = x.Timestamp, Response = x.ResponseTime.Value })
                .OrderBy(x => x.Time)
                .ToArrayAsync();
        }

        public async Task<double?> CalculateUptime(long endpointId, int days = 30)
        {
            var now = DateTime.UtcNow;
            var up = await _healthReportRepository
                .Query()
                .Where(x => x.EndpointId == endpointId 
                    && DbFunctions.DiffDays(x.Timestamp, now) < days
                    && x.StatusCode != null
                )
                .Select(x => x.IntervalInMinutes)
                .DefaultIfEmpty(0)
                .SumAsync();

            var total = await _healthReportRepository
                .Query()
                .Where(x => x.EndpointId == endpointId
                    && DbFunctions.DiffDays(x.Timestamp, now) < days
                )
                .Select(x => x.IntervalInMinutes)
                .DefaultIfEmpty(0)
                .SumAsync();
            if (total == 0)
            {
                return null;
            }

            return (up / (double)total) * 100d;
        }

        public async Task<StatusesAndErrors> GetStatistic(long endpointId, int days = 30)
        {
            var now = DateTime.UtcNow;
            IQueryable<HealthReport> fromLastDays = _healthReportRepository
                .Query()
                .Where(x => x.EndpointId == endpointId
                    && DbFunctions.DiffDays(x.Timestamp, now) < days
                );

            var codes = await fromLastDays
                .Where(x => x.StatusCode != null)
                .GroupBy(x => x.StatusCode)
                .ToDictionaryAsync(x => x.Key, x => x.Count());

            var errors = await fromLastDays
                .Where(x => x.Error != null)
                .GroupBy(x => x.Error)
                .ToDictionaryAsync(x => x.Key, x => x.Count());

            var total = await fromLastDays
                .CountAsync();

            var result = new StatusesAndErrors
            {
                TotalRequests = total,
                Since = DateTime.UtcNow.Subtract(TimeSpan.FromDays(days)),
                Errors = errors,
                StatusCodes = codes.Select(x => new { key = $"{x.Key} - { ((HttpStatusCode)x.Key).ToString()}", value = x.Value }).ToDictionary(x => x.key, x => x.value)
            };

            return result;
        }

        public async Task AddReport(HealthReport report)
        {
            _healthReportRepository.AddOrUpdate(report);
            await _uow.CommitAsync();
        }
        public void AddReportSyncroniously(HealthReport report)
        {
            _healthReportRepository.AddOrUpdate(report);
            _uow.Commit();

        }
    }
}
