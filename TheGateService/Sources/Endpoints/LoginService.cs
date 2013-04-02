using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.Text;

using TheGateService.Responses;
using TheGateService.Types;

namespace TheGateService.Endpoints {
    public class LoginService : GateServiceBase {
        public object Get(Login request) {
            return new LoginResponse();
        }

        public object Post(Login request) {
            try {
                AuthService.Authenticate(new Auth { UserName = request.Username, Password = request.Password });
            } catch (HttpError) {
                // Invalid username or password
                var message = "{0}:{1}".Fmt(request.Username, "Incorrect username or password.");
                Session.Set("Error-Message", message);
                return HttpResult.Redirect(Url.Content("~/Login"));
            }
            return HttpResult.Redirect(Request.QueryString["redirect"] ?? Url.Content("~/home"));
        }
    }
}
