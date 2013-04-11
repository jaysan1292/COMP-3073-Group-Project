using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using MySql.Data.MySqlClient;

using ServiceStack.Logging;

using TheGateService.Endpoints;
using TheGateService.Types;

namespace TheGateService.Database {
    public class ShoppingCartDbProvider {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ShoppingCartDbProvider));

        public ShoppingCart Get(User user) {
            return Get(user.Id);
        }

        public ShoppingCart Get(long userId) {
            MySqlTransaction tx;
            using (var conn = DbHelper.OpenConnectionAndBeginTransaction(out tx)) {
                try {
                    var cmd = new MySqlCommand {
                        Connection = conn,
                        CommandText = "GetCart",
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("UserId", userId);

                    var reader = cmd.ExecuteReader();
                    var cart = BuildObject(reader);
                    reader.Close();

                    return cart;
                } catch (Exception e) {
                    tx.Rollback();
                    Log.Error(e.Message, e);
                    throw;
                } finally {
                    DbHelper.CloseConnectionAndEndTransaction(conn, tx);
                }
            }
        }

        private ShoppingCart BuildObject(MySqlDataReader reader) {
            if (!reader.Read()) return null;
            var cart = new ShoppingCart();
            do {
                cart.Items.Add(new ShoppingCart.ShoppingCartItem {
                    Product = new Product {
                        Id = reader.GetInt64("ProductId"),
                        Name = reader.GetString("Name"),
                        Price = reader.GetDecimal("Price"),
                        Description = reader.GetString("Description"),
                    },
                    Quantity = reader.GetInt32("Quantity"),
                });
            } while (reader.Read());

            return cart;
        }
    }
}
