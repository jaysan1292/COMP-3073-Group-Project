﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

using MySql.Data.MySqlClient;

namespace TheGateService.Database {
    public static class DbHelper {
        [DebuggerHidden]
        public static MySqlConnection CreateConnection() {
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["thegate"].ConnectionString);
        }

        [DebuggerHidden]
        public static MySqlConnection OpenConnection() {
            var connection = CreateConnection();
            connection.Open();
            return connection;
        }

        [DebuggerHidden]
        public static void CloseConnection(MySqlConnection connection) {
            if (connection != null) connection.Close();
        }

        [DebuggerHidden]
        public static MySqlConnection OpenConnectionAndBeginTransaction(out MySqlTransaction transaction) {
            var connection = OpenConnection();
            transaction = connection.BeginTransaction();
            return connection;
        }

        [DebuggerHidden]
        public static void CloseConnectionAndEndTransaction(MySqlConnection connection, MySqlTransaction transaction) {
            if (transaction != null) transaction.Commit();
            CloseConnection(connection);
        }
    }
}
