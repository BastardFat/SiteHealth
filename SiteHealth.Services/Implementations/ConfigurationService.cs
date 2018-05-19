using AutoMapper;
using SiteHealth.Database.Concrete.Interfaces;
using SiteHealth.Database.Repos.Interfaces;
using SiteHealth.Entity.Models;
using SiteHealth.Services.Interfaces;
using SiteHealth.Services.ViewModels;
using SiteHealth.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Services.Implementations
{
    [NinjectDependency(typeof(IConfigurationService), NinjectDependencyScope.Request)]
    public class ConfigurationService : IConfigurationService
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IMapper _mapper;
        private readonly ISiteHealthUnitOfWork _uow;
        public ConfigurationService(
            ISiteRepository siteRepository,
            IMapper mapper,
            ISiteHealthUnitOfWork uow)
        {
            _siteRepository = siteRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<SiteViewModel> AddOrUpdateSite(SiteViewModel model)
        {
            var entity = _mapper.Map<Site>(model);
            entity = _siteRepository.AddOrUpdate(entity);
            await _uow.CommitAsync();
            var result = _mapper.Map<SiteViewModel>(entity);
            return result;
        }
    }
}
