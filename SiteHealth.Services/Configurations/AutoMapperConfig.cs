using AutoMapper;
using SiteHealth.Entity.Models;
using SiteHealth.Services.ViewModels;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Services.Configurations
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration CreateMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Site, SiteViewModel>()
                    .ReverseMap();

            });
        }
    }
}
