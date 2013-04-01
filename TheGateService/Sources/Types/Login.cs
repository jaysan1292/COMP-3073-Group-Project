using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceHost;

namespace TheGateService.Types {
    [Route("/login")]
    public class Login : Entity<Login> {
        public string Username { get; set; }
        public string Password { get; set; }

        protected override bool _Equals(Login other) {
            return Username == other.Username &&
                   Password == other.Password;
        }
    }
}
