using AutoMapper;
using AutoMapper.QueryableExtensions;
using SiteHealth.Database.Concrete.Interfaces;
using SiteHealth.Database.Repos.Interfaces;
using SiteHealth.Entity.Models;
using SiteHealth.Services.Implementations.Base;
using SiteHealth.Services.Interfaces;
using SiteHealth.Services.ViewModels.Site;
using SiteHealth.Services.ViewModels.Utils;
using SiteHealth.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.Services.Implementations
{
    [NinjectDependency(typeof(IEndpointService), NinjectDependencyScope.Request)]
    public class EndpointService : BaseService, IEndpointService
    {
        private readonly ISiteRepository _siteRepository;

        private readonly IMapper _simpleMapper;
        private readonly IMapper _detailedMapper;

        public EndpointService(
            ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;

            _simpleMapper = CreateSimpleMapper();
            _detailedMapper = CreateMapperWithDetails();
        }

        public async Task<SiteViewModelWithDetailedChilds> GetSite(long id)
        {
            var entity = await _siteRepository.GetByIdAsync(id);
            var result = _detailedMapper.Map<SiteViewModelWithDetailedChilds>(entity);
            return result;
        }

        public async Task<PagedDataSource<SiteViewModel>> GetSites(int page, string search)
        {
            if (search == null) search = String.Empty;

            var result = await _siteRepository
                .Query()
                .Where(x => x.Name.Contains(search))
                .ProjectTo<SiteViewModel>(_simpleMapper.ConfigurationProvider)
                .ConvertToPagedDataSourceAsync(page);

            return result;
        }

    }
}
