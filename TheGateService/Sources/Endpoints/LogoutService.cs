using System.Diagnostics;

using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;

namespace TheGateService.Endpoints {
    [Route("/logout")]
    public class Logout { }

    public class LogoutService : GateServiceBase {
        public object Any(Logout request) {
            var authResponse = AuthService.Authenticate(new Auth { provider = "logout" });
            Debugger.Break();
            return HttpResult.Redirect(Url.Content("~"));
        }
    }
}