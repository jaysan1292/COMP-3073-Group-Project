using System;

using MySql.Data.MySqlClient;

using TheGateService.Endpoints;

namespace TheGateService.Database {
    public class ShoppingCartDbProvider :BaseDbProvider<ShoppingCart> {
        public ShoppingCartDbProvider()
            : base("ShoppingCart") { }

        protected override ShoppingCart Get(long id, MySqlConnection conn) {
            throw new NotImplementedException();
        }

        protected override long Create(ShoppingCart obj, MySqlConnection conn) {
            throw new NotImplementedException();
        }

        protected override void Update(ShoppingCart obj, MySqlConnection conn) {
            throw new NotImplementedException();
        }

        protected override bool Delete(long id, MySqlConnection conn) {
            throw new NotImplementedException();
        }

        protected override ShoppingCart BuildObject(MySqlDataReader reader) {
            throw new NotImplementedException();
        }
    }
}