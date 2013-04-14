using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Validation;

using TheGateService.Responses;
using TheGateService.Types;
using TheGateService.Validation;

namespace TheGateService.Endpoints {
    public class UserRegisterService : GateServiceBase {
        public object Get(UserRegister request) {
            if (this.GetSession().IsAuthenticated) return HttpResult.Redirect(Url.Content("~/home"));
            return new UserRegisterResponse();
        }

        public object Post(UserRegister request) {
            throw new NotImplementedException();
        }
    }
}
