using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Common.Data
{
    public static class DbExtensions
    {
        public static string GetStringSafe(this DbDataReader reader, int index, string def = null)
        {
            if (reader.IsDBNull(index))
            {
                return def;
            }
            try
            {
                return reader.GetString(index);
            }
            catch
            {
                return reader.GetValue(index).ToString();
            }
        }

        public static string ReadSingleString(this IDbHelper helper, string sql, string key, string def = null)
        {
            using (var reader = helper.Query(sql))
            {
                if (reader != null && reader.Read())
                {
                    int index = reader.GetOrdinal(key);
                    return reader.GetString(index);
                }
            }
            return def;
        }
        public static long ReadSingleInt64(this IDbHelper helper, string sql, string key, long def = 0)
        {
            using (var reader = helper.Query(sql))
            {
                if (reader != null && reader.Read())
                {
                    int index = reader.GetOrdinal(key);
                    return reader.GetInt64(index);
                }
            }
            return def;
        }
        public static string GetObjectSafe(this DbDataReader reader, int index, string def = null)
        {
            if (reader.IsDBNull(index))
            {
                return def;
            }
            return reader.GetValue(index).ToString();
        }
        public static int GetInt32Safe(this DbDataReader reader, int index, int def = 0)
        {
            if (reader.IsDBNull(index))
            {
                return def;
            }
            return reader.GetInt32(index);
        }
      
        public static string ReadString(this DbDataReader reader, string col, string def = null)
        {
            object obj = reader[col];
            if (obj == DBNull.Value)
                return def;
            return Convert.ToString(obj);
        }
        public static short ReadInt16(this DbDataReader reader, string col, short def = 0)
        {
            object obj = reader[col];
            if (obj == DBNull.Value)
                return def;
            return Convert.ToInt16(obj);
        }
        public static int ReadInt32(this DbDataReader reader, string col, int def = 0)
        {
            object obj = reader[col];
            if (obj == DBNull.Value)
                return def;
            return Convert.ToInt32(obj);
        }
        public static bool ReadBoolean(this DbDataReader reader, string col, bool def = false)
        {
            object obj = reader[col];
            if (obj == DBNull.Value)
                return def;
            string val = Convert.ToString(obj).ToLower();
            return "1" == val || "true" == val;
        }
        public static long ReadInt64(this DbDataReader reader, string col, long def = 0)
        {
            object obj = reader[col];
            if (obj == DBNull.Value)
                return def;
            return Convert.ToInt64(obj);
        }

        public static T ReadEnum<T>(this DbDataReader reader, string col, T def = default(T))
        {
            object obj = reader[col];
            string val = "";
            if (obj != DBNull.Value)
            {
                val = Convert.ToString(obj);
            }
            T e;
            try
            {
                e = (T)Enum.Parse(typeof(T), val);
            }
            catch
            {
                e = def;
            }
            return e;
        }
    }
}
