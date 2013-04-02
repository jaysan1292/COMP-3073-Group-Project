using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.Text;

using TheGateService.Database;
using TheGateService.Types;

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
            var perms = new List<string>();
            switch (user.Type) {
                case UserType.User:
                    // Users can view products, but so can everyone else, so they don't get a specific permission
                    break;
                case UserType.BasicEmployee:
                    perms.Add(Permissions.CanCreateOrders);
                    perms.Add(Permissions.CanManageProducts);
                    break;
                case UserType.Shipping:
                    break;
                case UserType.Administrator:
                    // Administrators GET ALL THE PERMISSIONS
                    perms.AddRange(typeof(Permissions).GetFields()
                                                      .Where(x => x.FieldType == typeof(string) && x.IsLiteral)
                                                      .ToList().Select(x => x.Name));
                    break;
            }
            session.Permissions = perms;

            return true;
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
