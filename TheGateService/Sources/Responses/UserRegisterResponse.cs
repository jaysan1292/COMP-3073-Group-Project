using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TheGateService.Types;

namespace TheGateService.Responses {
    public class UserRegisterResponse : ResponseBase {
        public UserRegister Request { get; set; }
    }
}
