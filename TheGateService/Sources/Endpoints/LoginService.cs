using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceInterface;
using ServiceStack.Text;

using TheGateService.Types;

namespace TheGateService.Endpoints {
    public class LoginService : Service {
        public object Post(Login request) {
            Global.Log.Debug(request.Dump());
            return request;
        }
    }
}
