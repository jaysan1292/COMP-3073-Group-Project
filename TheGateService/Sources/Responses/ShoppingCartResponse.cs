using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;

using TheGateService.Types;

namespace TheGateService.Endpoints {
    public class ShoppingCartResponse : ResponseBase {
        public ShoppingCart Cart;
    }
}