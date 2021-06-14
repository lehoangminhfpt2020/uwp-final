using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace Dm1.Helper
{
    class SQLiteHelper
    {
        private static SQLiteHelper _instance;
        public SQLiteConnection SQLiteConnection { get; set; }

        public static SQLiteHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SQLiteHelper();
            }
            return _instance;
        }

        public SQLiteHelper()
        {
            SQLiteConnection = new SQLiteConnection(Const.DATABASE_NAME);
            CreateTables();
        }

        private void CreateTables()
        {
            CreateUseTable();
        }

        private void CreateUseTable()
        {
            var q = @"CREATE TABLE IF NOT EXISTS users(id integer auto_increment primary key, username varchar(200) unique, password varchar(200)";
            var s = SQLiteConnection.Prepare(q);
            s.Step();
        }
    }
}
