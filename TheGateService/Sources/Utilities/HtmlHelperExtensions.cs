using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.CacheAccess;
using ServiceStack.Html;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;

namespace TheGateService.Utilities {
    public static class HtmlHelperExtensions {
        public static IAuthSession GetAuthSession(this HtmlHelper html) {
            IAuthSession session;
            var cont = AppHostBase.Instance.Container;
            var cache = cont.Resolve<ICacheClient>();
            var req = html.GetHttpRequest();
            try {
                session = cache.GetSession(req.QueryString["sessionId"] ?? req.Cookies["ss-id"].Value);
            } catch (KeyNotFoundException) {
                session = SessionFeature.GetOrCreateSession<AuthUserSession>(cache);
            }

            return session;
        }

        public static bool IsRelease(this HtmlHelper html) {
#if DEBUG
            return false;
#else
            return true;
#endif
        }

        public static long GetUserId(this HtmlHelper html) {
            return Convert.ToInt64(html.GetAuthSession().UserAuthId);
        }
    }
}
