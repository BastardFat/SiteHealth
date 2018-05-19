using SiteHealth.Entity.Models;
using SiteHealth.Services.ViewModels.Site;
using SiteHealth.Services.ViewModels.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteHealth.Services.Interfaces
{
    public interface IEndpointService
    {
        Task<PagedDataSource<SiteViewModel>> GetSites(int page, string search);
    }
}
