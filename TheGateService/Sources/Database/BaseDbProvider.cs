using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using MySql.Data.MySqlClient;

using ServiceStack.Logging;

using TheGateService.Extensions;
using TheGateService.Types;

namespace TheGateService.Database {
    public abstract class BaseDbProvider<T> : IDbProvider<T> where T : Entity<T> {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(BaseDbProvider<T>));
        private readonly string _tableName;

        protected BaseDbProvider(string tableName) {
            _tableName = tableName;
        }

        public T Get(long id) {
            MySqlTransaction tx;
            using (var conn = DbHelper.OpenConnectionAndBeginTransaction(out tx)) {
                try {
                    return Get(id, conn);
                } catch (Exception e) {
                    tx.Rollback();
                    Log.Error(e.Message, e);
                    throw;
                } finally {
                    DbHelper.CloseConnectionAndEndTransaction(conn, tx);
                }
            }
        }

        public long Create(T obj) {
            MySqlTransaction tx;
            using (var conn = DbHelper.OpenConnectionAndBeginTransaction(out tx)) {
                try {
                    return Create(obj, conn);
                } catch (Exception e) {
                    tx.Rollback();
                    Log.Error(e.Message, e);
                    throw;
                } finally {
                    DbHelper.CloseConnectionAndEndTransaction(conn, tx);
                }
            }
        }

        public void Update(T obj) {
            MySqlTransaction tx;
            using (var conn = DbHelper.OpenConnectionAndBeginTransaction(out tx)) {
                try {
                    Update(obj, conn);
                } catch (Exception e) {
                    tx.Rollback();
                    Log.Error(e.Message, e);
                    throw;
                } finally {
                    DbHelper.CloseConnectionAndEndTransaction(conn, tx);
                }
            }
        }

        public bool Delete(long id) {
            MySqlTransaction tx;
            using (var conn = DbHelper.OpenConnectionAndBeginTransaction(out tx)) {
                try {
                    return Delete(id, conn);
                } catch (Exception e) {
                    tx.Rollback();
                    Log.Error(e.Message, e);
                    throw;
                } finally {
                    DbHelper.CloseConnectionAndEndTransaction(conn, tx);
                }
            }
        }

        public List<T> GetAll() {
            MySqlTransaction tx;
            using (var conn = DbHelper.OpenConnectionAndBeginTransaction(out tx)) {
                var cmd = new MySqlCommand {
                    Connection = conn,
                    CommandText = "SELECT * FROM {0}".F(_tableName)
                };
                var reader = cmd.ExecuteReader();
                var output = new List<T>();
                while (true) {
                    var prod = BuildObject(reader);
                    if (prod == null) break;
                    output.Add(prod);
                }
                return output;
            }
        }

        protected abstract T Get(long id, MySqlConnection conn);
        protected abstract long Create(T obj, MySqlConnection conn);
        protected abstract void Update(T obj, MySqlConnection conn);
        protected abstract bool Delete(long id, MySqlConnection conn);
        protected abstract T BuildObject(MySqlDataReader reader);
    }
}
