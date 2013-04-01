using System.Collections.Generic;
using System.Web;

using ServiceStack.Html;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.WebHost.Endpoints.Extensions;

namespace TheGateService.Endpoints {
    public class GateServiceBase : Service {
        private readonly UrlHelper _url = new UrlHelper();
        public UrlHelper Url { get { return _url; } }

        public IAuthSession AuthSession {
            get {
                try {
                    // If sessionId is not provided in the query string, check if a cookie was sent instead
                    return base.Cache.GetSession(Request.QueryString["sessionId"] ?? Request.Cookies["ss-id"].Value);
                } catch (KeyNotFoundException) {
                    return SessionFeature.GetOrCreateSession<AuthUserSession>(Cache);
                }
            }
        }

        public AuthService AuthService {
            get {
                var authService = AppHostBase.Instance.Container.Resolve<AuthService>();
                authService.RequestContext = new HttpRequestContext(HttpContext.Current.Request.ToRequest(),
                                                                    HttpContext.Current.Response.ToResponse(),
                                                                    null);
                return authService;
            }
        }
    }
}