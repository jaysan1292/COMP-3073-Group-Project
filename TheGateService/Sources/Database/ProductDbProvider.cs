using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using MySql.Data.MySqlClient;

using TheGateService.Types;

namespace TheGateService.Database {
    public class ProductDbProvider : BaseDbProvider<Product> {
        private const string Query = "SELECT * FROM Product WHERE ProductId=@id";

        public ProductDbProvider()
            : base("Product") { }

        protected override Product BuildObject(MySqlDataReader reader) {
            if (!reader.Read()) return null;
            return new Product {
                Id = reader.GetInt64("ProductId"),
                Name = reader.GetString("Name"),
                Description = reader.GetString("Description"),
                Quantity = reader.GetInt32("Quantity"),
                Price = reader.GetDecimal("Price"),
            };
        }

        protected override Product Get(long id, MySqlConnection conn) {
            var cmd = new MySqlCommand {
                Connection = conn,
                CommandText = Query,
            };
            cmd.Parameters.AddWithValue("id", id);

            var reader = cmd.ExecuteReader();
            var product = BuildObject(reader);
            reader.Close();

            if (product == null) return null;

            return product;
        }

        protected override long Create(Product obj, MySqlConnection conn) {
            throw new NotImplementedException();
        }

        protected override void Update(Product obj, MySqlConnection conn) {
            throw new NotImplementedException();
        }

        protected override void Delete(long id, MySqlConnection conn) {
            throw new NotImplementedException();
        }
    }
}
