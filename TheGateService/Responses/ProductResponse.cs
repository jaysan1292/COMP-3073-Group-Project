// Project: TheGateService
// Filename: ProductResponse.cs
// 
// Author: Jason Recillo

using System;

using TheGateService.Types;

namespace TheGateService.Responses {
    public class ProductResponse : ResponseBase {
        public Product Product { get; set; }
    }
}
