using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TheGateService.Types;

namespace TheGateService.Responses {
    public class AdminPanelResponse : ResponseBase {
        public List<Product> Results { get; set; }
        /*public AdminPanel AdminPanel { get; set; }
        public AdminPanelResponse() { }

        public AdminPanelResponse(AdminPanel panel) {
            AdminPanel = panel;
        }*/
    }
}