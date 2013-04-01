using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceInterface;

using TheGateService.Responses;
using TheGateService.Types;
using TheGateService.Responses;
using TheGateService.Database;

namespace TheGateService.Endpoints {
    public class AdminPanelService : Service {
        public object Get(AdminPanel request) {
            return new AdminPanelResponse { Results = new ProductDbProvider().GetAll() };
        }
    }
}