using System;
using System.Collections.Generic;

using TheGateService.Types;

namespace TheGateService.Responses {
    public class ProductsResponse : ResponseBase {
        public List<Product> Results { get; set; }
    }
}