using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;

namespace TheGateService.Endpoints {
    [Route("/logout")]
    public class Logout { }

    public class LogoutService : GateServiceBase {
        public object Any(Logout request) {
            var authResponse = AuthService.Authenticate(new Auth { provider = "logout" });
            return HttpResult.Redirect(Url.Content("~"));
        }
    }
}
