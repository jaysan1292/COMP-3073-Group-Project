// Project: TheGateService
// Filename: Global.asax.cs
// 
// Author: Jason Recillo

using System;
using System.Web;

using Funq;

using ServiceStack.CacheAccess;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.Logging;
using ServiceStack.Logging.NLogger;
using ServiceStack.MiniProfiler;
using ServiceStack.Razor;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

namespace TheGateService {
    public class GateServiceHost : AppHostBase {
        public GateServiceHost()
            : base("The Gate", typeof(GateServiceHost).Assembly) { }

        public override void Configure(Container container) {
            Global.Log.Info("Configuring application...");

            SetConfig(new EndpointHostConfig {
                EnableFeatures = Feature.All.Remove(Feature.Soap),
                DefaultContentType = ContentType.Json,
                DebugMode = true,
                DefaultRedirectPath = "/home"
            });

            Plugins.Add(new RazorFormat());

            JsConfig.EmitCamelCaseNames = true;

            Global.Log.Info("Application configured!");
        }
    }

    public class Global : HttpApplication {
        public static ICacheClient Cache;
        public static ILog Log;

        protected void Application_Start(object sender, EventArgs e) {
            LogManager.LogFactory = new NLogFactory();
            (Log = LogManager.GetLogger(typeof(Global))).Info("Application start!");
            new GateServiceHost().Init();
        }

        protected void Session_Start(object sender, EventArgs e) { }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            if (Request.IsLocal) Profiler.Start(ProfileLevel.Verbose);
        }

        protected void Application_EndRequest(object sender, EventArgs e) {
            if (Request.IsLocal) Profiler.Stop();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) { }

        protected void Application_Error(object sender, EventArgs e) { }

        protected void Session_End(object sender, EventArgs e) { }

        protected void Application_End(object sender, EventArgs e) { }
    }
}
