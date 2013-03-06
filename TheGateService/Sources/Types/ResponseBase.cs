// Project: TheGateService
// Filename: ResponseBase.cs
// 
// Author: Jason Recillo

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceInterface.ServiceModel;

namespace TheGateService.Types {
    public class ResponseBase {
        public ResponseStatus ResponseStatus { get; set; }
    }
}
