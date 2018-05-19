using SiteHealth.Services.ViewModels.Endpoint;
using SiteHealth.Services.ViewModels.Site;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteHealth.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<SiteViewModel> SaveSite(SiteViewModel model);
        Task RemoveSite(long id);

        Task<EndpointViewModel> SaveEndpoint(EndpointViewModel model);
        Task RemoveEndpoint(long id);

        Task<T> GetOption<T>(string key, T defaultValue = default(T));
        Task<object> GetOption(string key);
        Task SetOption(string key, object value, string type);
        Task<Dictionary<string, object>> GetOptions();
    }
}
