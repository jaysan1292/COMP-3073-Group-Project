using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;

using MySql.Data.MySqlClient;

using ServiceStack.ServiceClient.Web;

using TheGateService.Types;

namespace TheGateService.Database {
    public class UserDbProvider : BaseDbProvider<User> {
        public UserDbProvider()
            : base("User") { }

        public string GetPassword(string useremail, out long userId) {
            MySqlTransaction tx;
            MySqlDataReader reader = null;
            using (var conn = DbHelper.OpenConnectionAndBeginTransaction(out tx)) {
                try {
                    var cmd = new MySqlCommand {
                        Connection = conn,
                        CommandText = "GetUserPassword",
                        CommandType = CommandType.StoredProcedure,
                    };
                    cmd.Parameters.AddWithValue("Email", useremail);

                    reader = cmd.ExecuteReader();
                    string password;

                    if (reader.Read()) {
                        password = reader.GetString("Password");
                        userId = reader.GetInt64("UserId");
                    } else {
                        userId = -1;
                        return null;
                    }

                    return password;
                } catch (Exception e) {
                    tx.Rollback();
                    Log.Error(e.Message, e);
                    throw;
                } finally {
                    if (reader != null) reader.Close();
                    DbHelper.CloseConnectionAndEndTransaction(conn, tx);
                }
            }
        }

        protected override User Get(long id, MySqlConnection conn) {
            var cmd = new MySqlCommand {
                Connection = conn,
                CommandText = "GetUserInfo",
                CommandType = CommandType.StoredProcedure,
            };
            cmd.Parameters.AddWithValue("UserId", id);

            var reader = cmd.ExecuteReader();
            var user = BuildObject(reader);
            reader.Close();

            return user; // User will be null if not found
        }

        protected override long Create(User obj, MySqlConnection conn) {
            throw new NotImplementedException();
        }

        protected override void Update(User obj, MySqlConnection conn) {
            throw new NotImplementedException();
        }

        protected override void Delete(long id, MySqlConnection conn) {
            throw new NotImplementedException();
        }

        protected override User BuildObject(MySqlDataReader reader) {
            if (!reader.Read()) return null;
            var user = new User {
                Id = reader.GetInt16("UserId"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                Address = reader.GetString("Address"),
                Email = reader.GetString("Email"),
            };
            switch (reader.GetInt32("Type")) {
                case 1:
                    user.Type = UserType.User;
                    break;
                case 2:
                    user.Type = UserType.BasicEmployee;
                    break;
                case 3:
                    user.Type = UserType.Shipping;
                    break;
                case 4:
                    user.Type = UserType.Administrator;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return user;
        }
    }
}
