using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TheGateService.Types;

namespace TheGateService.Responses {
    public class LoginResponse : ResponseBase {
        public Login Credentials { get; set; }
        public LoginResponse() { }

        public LoginResponse(Login login) {
            Credentials = login;
        }
    }
}
