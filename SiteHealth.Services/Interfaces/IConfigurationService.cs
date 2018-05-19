using SiteHealth.Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteHealth.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<SiteViewModel> AddOrUpdateSite(SiteViewModel model);
    }
}
