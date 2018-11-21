using DausterCustomer.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DausterCustomer.DataBase
{
    public class dbLogic
    {
        readonly SQLiteAsyncConnection database;

        public dbLogic(string dbPath) {
            database = new SQLiteAsyncConnection(dbPath);
        }
    }
}
