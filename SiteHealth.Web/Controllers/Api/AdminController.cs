using BotMagic.Utils;
using Hangfire;
using SiteHealth.Controllers.Base;
using SiteHealth.Filters;
using SiteHealth.Services.Interfaces;
using SiteHealth.Services.ViewModels.Endpoint;
using SiteHealth.Services.ViewModels.Site;
using SiteHealth.Web.Background;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SiteHealth.Web.Controllers.Api
{
    [RoutePrefix("api/admin")]
    public class AdminController : BaseApiController
    {
        private readonly IConfigurationService _configurationService;
        private readonly IHmacSerializer<string> _serializer;
        private readonly ISha256Hasher _hasher;
        public AdminController(IConfigurationService configurationService,
            IHmacSerializer<string> serializer,
            ISha256Hasher hasher)
        {
            _configurationService = configurationService;
            _serializer = serializer;
            _hasher = hasher;
        }

        [OnlyAuthorize]
        [HttpPost]
        [Route("site/save")]
        public async Task<SiteViewModelWithChilds> SaveSite(SiteViewModelWithChilds model)
        {
            var res = await _configurationService.SaveSite(model);
            await BackgroundJobManager.UpdateWorker(res.Id, _configurationService);
            return res;
        }

        [OnlyAuthorize]
        [HttpDelete]
        [Route("site/remove")]
        public async Task RemoveSite(long id)
        {
            await _configurationService.RemoveSite(id);
            BackgroundJobManager.RemoveWorker(id);
        }

        [OnlyAuthorize]
        [HttpGet]
        [Route("site/edit")]
        public async Task<SiteViewModelWithChilds> EditSite(long id)
        {
            return await _configurationService.GetSite(id);
        }

        [OnlyAuthorize]
        [HttpPost]
        [Route("options/set")]
        public async Task SetOption([FromUri] string key, [FromUri] string type, [FromBody] object model)
        {
            await _configurationService.SetOption(key, model, type);
        }

        [OnlyAuthorize]
        [HttpGet]
        [Route("options/get")]
        public async Task<Dictionary<string, object>> GetOptions()
        {
            return await _configurationService.GetOptions();
        }

        [OnlyAuthorize]
        [HttpGet]
        [Route("options/get")]
        public async Task<object> GetOption(string key)
        {
            return await _configurationService.GetOption(key);
        }

        [HttpGet]
        [Route("password/authorize")]
        public async Task<string> GetToken(string password)
        {
            string truePassword = await _configurationService.GetOption<string>("password");
            if (password != truePassword)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("Invalid password") });
            return _serializer.Serialize(password);
        }

        [HttpGet]
        [Route("password/change")]
        public async Task<string> ChangePassword(string password, string newPassword)
        {
            string truePassword = await _configurationService.GetOption<string>("password");
            if (password != truePassword)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent("Invalid password") });
            await _configurationService.SetOption("password", newPassword, typeof(string).FullName);
            return _serializer.Serialize(newPassword);
        }

    }
}