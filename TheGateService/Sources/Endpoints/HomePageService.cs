using System;

using ServiceStack.ServiceInterface;

using TheGateService.Responses;
using TheGateService.Types;

namespace TheGateService.Endpoints {
    public class HomePageService : Service {
        public object Get(HomePage request) {
            return new HomePageResponse { HomePage = new HomePage() };
        }
    }
}