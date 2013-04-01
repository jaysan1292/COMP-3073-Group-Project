using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TheGateService.Types;

namespace TheGateService.Responses {
    public class ProductResponse : ResponseBase {
        public Product Product { get; set; }
    }
}
