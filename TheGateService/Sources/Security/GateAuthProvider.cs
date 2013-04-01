using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.Common;
using ServiceStack.Html;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.Text;

using TheGateService.Database;

namespace TheGateService.Security {
    public class GateAuthProvider : CredentialsAuthProvider {
        private static readonly UserDbProvider Users = new UserDbProvider();

        public override bool TryAuthenticate(IServiceBase authService, string userName, string password) {
            long userId;
            if (!AuthenticateUser(userName, password, out userId)) return false;

            var user = Users.Get(userId);
            var session = authService.GetSession();
            session.IsAuthenticated = true;
            session.UserAuthId = Convert.ToString(userId);
            session.UserAuthName = userName;
            session.UserName = "{0} {1}".Fmt(user.FirstName, user.LastName);
            session.FirstName = user.FirstName;
            session.LastName = user.LastName;
            session.Email = user.Email;
            session.Roles = new List<string> {
                user.Type.ToString()
            };

            return true;
        }

        public override void OnFailedAuthentication(IAuthSession session, IHttpRequest httpReq, IHttpResponse httpRes) {
            httpRes.AddHeader("X-Error-Message", "Incorrect username or password.");
            httpRes.Redirect(new UrlHelper().Content("~/Login"));
        }

        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IOAuthTokens tokens, Dictionary<string, string> authInfo) {
            authService.SaveSession(session);
        }

        private static bool AuthenticateUser(string username, string password, out long userId) {
            var encryptedpass = Users.GetPassword(username, out userId);

            // if encryptedpass is not null and the password is correct, return true
            return encryptedpass != null && PasswordHelper.CheckPassword(password, encryptedpass);
        }
    }
}
