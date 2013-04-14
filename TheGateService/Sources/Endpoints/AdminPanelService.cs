using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceInterface;

using TheGateService.Database;
using TheGateService.Responses;
using TheGateService.Types;

namespace TheGateService.Endpoints {
    [Authenticate]
    [RequiredRole("Administrator")]
    public class AdminPanelService : Service {
        public object Get(AdminPanel request) {
            return new AdminPanelResponse { Results = new ProductDbProvider().GetAll() };
        }

        public object Post(AdminPanel request) {
            Global.Log.Debug("Post");
            foreach (var p in request.Products)
                Global.Log.Debug(p.Id);

            return null;
        }

        public object Delete(AdminPanel request) {
            Global.Log.Debug("Delete");
            var id = Request.FormData["id"];
            Global.Log.Debug(id);

            return null;
        }
    }
}
