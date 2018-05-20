using AutoMapper;
using SiteHealth.Database.Concrete.Interfaces;
using SiteHealth.Database.Repos.Interfaces;
using SiteHealth.Entity.Models;
using SiteHealth.Services.Implementations.Base;
using SiteHealth.Services.Interfaces;
using SiteHealth.Services.ViewModels.Endpoint;
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
    [NinjectDependency(typeof(IConfigurationService), NinjectDependencyScope.Request)]
    public class ConfigurationService : BaseService, IConfigurationService
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IOptionRepository _optionRepository;
        private readonly IEndpointRepository _endpointRepository;
        private readonly ISiteHealthUnitOfWork _uow;

        private readonly IMapper _simpleMapper;
        private readonly IMapper _deepMapper;

        public ConfigurationService(
            ISiteRepository siteRepository,
            IOptionRepository optionRepository,
            IEndpointRepository endpointRepository,
            ISiteHealthUnitOfWork uow)
        {
            _siteRepository = siteRepository;
            _optionRepository = optionRepository;
            _endpointRepository = endpointRepository;
            _uow = uow;

            _simpleMapper = CreateSimpleMapper();
            _deepMapper = CreateMapperWithChilds();
        }

        public async Task<SiteViewModelWithChilds> SaveSite(SiteViewModelWithChilds model)
        {
            Site entity;
            if (model.Id == 0)
            {
                entity = _deepMapper.Map<Site>(model);
                entity.CreatedAt = DateTime.UtcNow;
                entity = _siteRepository.Add(entity);
            }
            else
            {
                entity = _simpleMapper.Map<Site>(model);
                entity.UpdatedAt = DateTime.UtcNow;
                entity = _siteRepository.Update(entity);
                var modelEndpoints = model.Endpoints.Select(x => x.Id).ToArray();

                var removed =await _endpointRepository.Query()
                    .Where(x => x.SiteId == model.Id && !modelEndpoints.Any(me => me == x.Id)).ToListAsync();

                _endpointRepository.Delete(removed);

                model.Endpoints.ForEach(x => x.SiteId = model.Id);
                var endpoints = _simpleMapper.Map<Endpoint[]>(model.Endpoints);
                endpoints = _endpointRepository.AddOrUpdate(endpoints).ToArray();
            }

            
            
            await _uow.CommitAsync();
            var result = _deepMapper.Map<SiteViewModelWithChilds>(entity);
            return result;
        }

        public async Task RemoveSite(long id)
        {
            _siteRepository.Delete(id);
            await _uow.CommitAsync();
        }


        public async Task<SiteViewModelWithChilds> GetSite(long id)
        {
            var entity = await _siteRepository.GetByIdAsync(id);
            var result = _deepMapper.Map<SiteViewModelWithChilds>(entity);
            return result;
        }

        #region Options

        public async Task<T> GetOption<T>(string key, T defaultValue = default(T))
        {
            var option = await _optionRepository.Query()
                .Where(x => x.Key == key && x.Type == typeof(T).FullName)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (option == null)
                return defaultValue;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(option.Value);
            return result;
        }

        public async Task<object> GetOption(string key)
        {
            var option = await _optionRepository.Query()
                .Where(x => x.Key == key)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (option == null)
                return null;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject(option.Value, Type.GetType(option.Type));
            return result;
        }

        public async Task SetOption(string key, object value, string type)
        {
            if (String.IsNullOrWhiteSpace(type))
                type = typeof(object).FullName;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            var option = _optionRepository.Add(new Option
            {
                Key = key,
                CreatedAt = DateTime.UtcNow,
                Type = type,
                Value = json
            });

            await _uow.CommitAsync();
        }

        public async Task<Dictionary<string, object>> GetOptions()
        {
            var options = await _optionRepository.Query()
                .GroupBy(x => x.Key)
                .Select(g => g.OrderByDescending(x => x.CreatedAt).FirstOrDefault()).ToArrayAsync();

            var result = options.ToDictionary(x => x.Key, x => Newtonsoft.Json.JsonConvert.DeserializeObject(x.Value, Type.GetType(x.Type)));

            return result;
        }

        #endregion
    }
}
