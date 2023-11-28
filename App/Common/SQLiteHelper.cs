//using SQLiteConnection = Microsoft.Data.Sqlite.SqliteConnection;
//using SQLiteCommand = Microsoft.Data.Sqlite.SqliteCommand;
using Common.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;

namespace SQLiteCompare.Data
{
    public class SQLiteHelper : DbHelper<SQLiteConnection>
    {
        public string File { get; }
        public SQLiteHelper() : base()
        {
        }
        public SQLiteHelper(string path) : base(GetConnectedStr(path, null))
        {
            File = path;
            string key = "Data Source=";
            int index = path.IndexOf(key);
            if (index < 0)
            {
                key = "Data Source =";
                index = path.IndexOf(key);
            }
            if (index >= 0)
            {
                int end = path.IndexOf(";", index);
                if (end > 0)
                {
                    File = path.Substring(key.Length, end - key.Length);
                }
                else
                {
                    File = path.Substring(key.Length);
                }
            }
        }

        public static string GetConnectedStr(string path, string pwd)
        {
            if (path.Contains("Data Source="))
            {
                return path;
            }
            string connStr = "Data Source=" + path;
            if (!string.IsNullOrEmpty(pwd))
            {
                connStr += ";Password=" + pwd;
            }
            return connStr;
        }
        public override Dictionary<string, string> GetColumns(string table)
        {
            var result = new Dictionary<string, string>();
            using (var reader = GetReader("PRAGMA table_info(\"" + table + "\");"))
            {
                int index = -1;
                int index_type = -1;
                while (reader.Read())
                {
                    if(index == -1)
                    {
                        index = reader.GetOrdinal("name");
                        index_type = reader.GetOrdinal("type");
                    }
                    result.Add(reader.GetString(index), reader.GetString(index_type));
                }
            }
            return result;
        }
        protected override SQLiteConnection ConnectDataBase(string connStr)
        {
            SQLiteConnection conn = new SQLiteConnection(connStr);
            try
            {
                conn.Open();
                using (DbTransaction tr = conn.BeginTransaction())
                {
                    tr.Rollback();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(connStr);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                conn.Close();
                conn = null;
            }
            return conn;
        }
        protected override DbCommand CreateCommand(string SQL, SQLiteConnection conn)
        {
            return new SQLiteCommand(SQL, conn);
        }

        public static bool CreateDataBase(string path)
        {
            SQLiteConnection.CreateFile(path);
            return System.IO.File.Exists(path);
            /*using (var conn = new SQLiteHelper(path))
            {
                if (conn.Open())
                {
                    conn.ExcuteSQL("create table __null__(id INTEGER); drop table __null__;");
                    conn.Close();
                    return true;
                } else
                {
                    return false;
                }
;            }*/
        }

        public bool ChangePassword(string pwd)
        {
            try
            {
                Connection.ChangePassword(pwd);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
