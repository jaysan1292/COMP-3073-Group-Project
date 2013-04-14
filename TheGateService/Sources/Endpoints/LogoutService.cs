using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface.Auth;

using TheGateService.Types;

namespace TheGateService.Endpoints {
    public class LogoutService : GateServiceBase {
        public object Any(Logout request) {
            var authResponse = AuthService.Authenticate(new Auth { provider = "logout" });
            return HttpResult.Redirect(Url.Content("~"));
        }
    }
}
