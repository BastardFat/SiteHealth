using SiteHealth.Services.ViewModels.Endpoint;
using SiteHealth.Services.ViewModels.Site;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteHealth.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<SiteViewModelWithChilds> SaveSite(SiteViewModelWithChilds model);
        Task RemoveSite(long id);
        Task<SiteViewModelWithChilds> GetSite(long id);

        Task<T> GetOption<T>(string key, T defaultValue = default(T));
        Task<object> GetOption(string key);
        Task SetOption(string key, object value, string type);
        Task<Dictionary<string, object>> GetOptions();
    }
}
