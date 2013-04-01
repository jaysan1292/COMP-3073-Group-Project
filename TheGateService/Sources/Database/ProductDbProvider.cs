using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using MySql.Data.MySqlClient;

using ServiceStack.Text;

using TheGateService.Types;

namespace TheGateService.Database {
    public class ProductDbProvider : BaseDbProvider<Product> {
        public ProductDbProvider()
            : base("Product") { }

        protected override Product Get(long id, MySqlConnection conn) {
            var cmd = new MySqlCommand {
                Connection = conn,
                CommandText = "CALL GetProductById(@id)",
            };
            cmd.Parameters.AddWithValue("id", id);

            var reader = cmd.ExecuteReader();
            var product = BuildObject(reader);
            reader.Close();

            return product; // Product will be null if not found
        }

        protected override long Create(Product obj, MySqlConnection conn) {
            var cmd = new MySqlCommand {
                Connection = conn,
                CommandText = "AddProduct",
                CommandType = CommandType.StoredProcedure,
            };
            cmd.Parameters.AddWithValue("ProdName", obj.Name);
            cmd.Parameters.AddWithValue("ProdDesc", obj.Description);
            cmd.Parameters.AddWithValue("ProdQuantity", obj.Quantity);
            cmd.Parameters.AddWithValue("ProdPrice", obj.Price);
            cmd.Parameters.AddWithValue("ProdFeatured", obj.Featured);
            cmd.Parameters.AddWithValue("ProdShowcase", obj.Showcase);
            cmd.Parameters.AddWithValue("NewId", MySqlDbType.Int64);
            cmd.Parameters["NewId"].Direction = ParameterDirection.Output;

            var rows = cmd.ExecuteNonQuery();

            if (rows != 1) throw new ApplicationException("Could not create new product.");

            var newid = Convert.ToInt64(cmd.Parameters["NewId"].Value);
            return newid;
        }

        protected override void Update(Product obj, MySqlConnection conn) {
            var cmd = new MySqlCommand {
                Connection = conn,
                CommandText = "UpdateProduct",
                CommandType = CommandType.StoredProcedure,
            };
            cmd.Parameters.AddWithValue("ProdId", obj.Id);
            cmd.Parameters.AddWithValue("ProdName", obj.Name);
            cmd.Parameters.AddWithValue("ProdDesc", obj.Description);
            cmd.Parameters.AddWithValue("ProdQuantity", obj.Quantity);
            cmd.Parameters.AddWithValue("ProdPrice", obj.Price);
            cmd.Parameters.AddWithValue("ProdFeatured", obj.Featured);
            cmd.Parameters.AddWithValue("ProdShowcase", obj.Showcase);

            var rows = cmd.ExecuteNonQuery();

            if (rows != 1) throw new ApplicationException("Could not update product {0}.".Fmt(obj.Id));
        }

        protected override void Delete(long id, MySqlConnection conn) {
            var cmd = new MySqlCommand {
                Connection = conn,
                CommandText = "DeleteProduct",
                CommandType = CommandType.StoredProcedure,
            };
            cmd.Parameters.AddWithValue("ProdId", id);

            var rows = cmd.ExecuteNonQuery();

            if (rows != 1) throw new ApplicationException("Could not delete product {0}.".Fmt(id));
        }

        protected override Product BuildObject(MySqlDataReader reader) {
            if (!reader.Read()) return null;
            return new Product {
                Id = reader.GetInt64("ProductId"),
                Name = reader.GetString("Name"),
                Description = reader.GetString("Description"),
                Quantity = reader.GetInt32("Quantity"),
                Price = reader.GetDecimal("Price"),
                Featured = reader.GetBoolean("Featured"),
                Showcase = reader.GetBoolean("Showcase"),
            };
        }
    }
}
