using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

using TheGateService.Types;

namespace TheGateService.Endpoints {
    [Route("/register", "GET,POST")]
    public class UserRegister {
        public User User { get; set; }
    }

    public class UserRegisterResponse : ResponseBase { }

    public class UserRegisterService : Service {
        public object Get(UserRegister request) {
            return new UserRegisterResponse();
        }

        public object Post(UserRegister request) {
            throw new NotImplementedException();
        }
    }
}
