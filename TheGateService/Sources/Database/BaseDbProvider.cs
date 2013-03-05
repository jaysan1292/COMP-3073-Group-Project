// Project: TheGateService
// Filename: BaseDbProvider.cs
// 
// Author: Jason Recillo

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using MySql.Data.MySqlClient;

using ServiceStack.Logging;

using TheGateService.Types;

namespace TheGateService.Database {
    public abstract class BaseDbProvider<T> : IDbProvider<T> where T : Entity<T> {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(BaseDbProvider<T>));

        public T Get(long id) {
            CheckIdIsValid(id);

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

        public void Delete(long id) {
            CheckIdIsValid(id);

            MySqlTransaction tx;
            using (var conn = DbHelper.OpenConnectionAndBeginTransaction(out tx)) {
                try {
                    Delete(id, conn);
                } catch (Exception e) {
                    tx.Rollback();
                    Log.Error(e.Message, e);
                    throw;
                } finally {
                    DbHelper.CloseConnectionAndEndTransaction(conn, tx);
                }
            }
        }

        protected abstract T Get(long id, MySqlConnection conn);
        protected abstract long Create(T obj, MySqlConnection conn);
        protected abstract void Update(T obj, MySqlConnection conn);
        protected abstract void Delete(long id, MySqlConnection conn);
        protected abstract T BuildObject(MySqlDataReader reader);

        [DebuggerHidden]
        protected void CheckIdIsValid(long id) {
            if (id < 0) throw new ArgumentException("Id must be non-negative.", "id");
        }
    }
}
