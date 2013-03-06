// Project: TheGateService
// Filename: HomePageResponse.cs
// 
// Author: Jason Recillo

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TheGateService.Types;

namespace TheGateService.Responses {
    public class HomePageResponse : ResponseBase {
        public HomePage HomePage { get; set; }
        public HomePageResponse() { }

        public HomePageResponse(HomePage page) {
            HomePage = page;
        }
    }
}
