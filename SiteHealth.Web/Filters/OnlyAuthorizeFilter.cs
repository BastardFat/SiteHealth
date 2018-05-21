using BotMagic.Utils;
using SiteHealth.Controllers.Base;
using SiteHealth.Ninject;
using SiteHealth.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SiteHealth.Filters
{
    public class OnlyAuthorizeAttribute : AuthorizeAttribute
    {
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var configService = GlobalDependencyResolver.GetService<IConfigurationService>();
            var serializer = GlobalDependencyResolver.GetService<IHmacSerializer<string>>();
            var scheme = actionContext.Request.Headers.Authorization?.Scheme;
            var token = actionContext.Request.Headers.Authorization?.Parameter;

            if (scheme != "Bearer" || token == null)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("Authorization required for this request") };
                return;
            }
            var password = serializer.Deserialize(token);
            var truePassword = await configService.GetOption<string>("password");
            if (password != truePassword)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("Invalid security token") };
                return;
            }
            ((BaseApiController)actionContext.ControllerContext.Controller).IsAdmin = true;
        }
    }
}