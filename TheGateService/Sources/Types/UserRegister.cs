using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceHost;

namespace TheGateService.Types {
    [Route("/register", "GET,POST")]
    public class UserRegister {
        public User User { get; set; }
    }
}
