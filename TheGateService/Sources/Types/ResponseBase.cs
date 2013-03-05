// Project: TheGateService
// Filename: ResponseBase.cs
// 
// Author: Jason Recillo

using ServiceStack.ServiceInterface.ServiceModel;

namespace TheGateService.Types {
    public class ResponseBase {
        public ResponseStatus ResponseStatus { get; set; }
    }
}