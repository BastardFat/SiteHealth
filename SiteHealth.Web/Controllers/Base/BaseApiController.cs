using System.Web.Http;

namespace SiteHealth.Controllers.Base
{
    public abstract class BaseApiController : ApiController
    {
        public bool IsAdmin { get; set; }
    }
}