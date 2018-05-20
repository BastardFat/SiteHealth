using AutoMapper;
using SiteHealth.Entity.Models;
using SiteHealth.Services.ViewModels.Endpoint;
using SiteHealth.Services.ViewModels.HealthReport;
using SiteHealth.Services.ViewModels.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Services.Implementations.Base
{
    public abstract class BaseService
    {
        protected IMapper CreateSimpleMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Site, SiteViewModel>()
                    .ReverseMap();

                cfg.CreateMap<Endpoint, EndpointViewModel>()
                    .ReverseMap();

                cfg.CreateMap<HealthReport, HealthReportViewModel>()
                    .ReverseMap();

            }).CreateMapper();
        }

        protected IMapper CreateMapperWithParents()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Site, SiteViewModel>()
                    .ReverseMap();

                cfg.CreateMap<Endpoint, EndpointViewModelWithParent>()
                    .ReverseMap();

                cfg.CreateMap<HealthReport, HealthReportViewModelWithParent>()
                    .ReverseMap();

            }).CreateMapper();
        }

        protected IMapper CreateMapperWithChilds()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Site, SiteViewModelWithChilds>()
                    .ReverseMap();

                cfg.CreateMap<Endpoint, EndpointViewModel>()
                    .ReverseMap();

                cfg.CreateMap<Endpoint, EndpointViewModelWithChilds>()
                    .ReverseMap();

                cfg.CreateMap<HealthReport, HealthReportViewModel>()
                    .ReverseMap();

            }).CreateMapper();
        }
    }
}
