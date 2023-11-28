using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Common.Data
{
    public interface IDbHelper
    {
        DbDataReader Query(string strSQL);
    }
    public abstract class DbHelper<T> : IDbHelper, IDisposable where T : DbConnection
    {
        #region
        public Exception Error { get; set; }
        protected T conn;
        protected string connStr;

        public bool IsOpen
        {
            get { return conn != null && conn.State == ConnectionState.Open; }
        }

        public bool IsClose
        {
            get { return conn == null; }
        }

        public DbHelper()
        {
            this.connStr = null;
        }
        public DbHelper(string connStr)
        {
            this.connStr = connStr;
        }


        protected abstract T ConnectDataBase(string connStr);

        public T Connection
        {
            get { return conn; }
        }

        protected abstract DbCommand CreateCommand(string SQL, T conn);


        public bool Open()
        {
            return Open(this.connStr);
        }

        public bool Open(string connStr)
        {
            if (connStr == null)
            {
                return false;
            }
            if (IsOpen)
            {
                Close();
            }
            this.connStr = connStr;
            try
            {
                conn = ConnectDataBase(connStr);
            }
            catch (Exception e)
            {
                conn = null;
                Error = e;
                return false;
            }
            return IsOpen;
        }

        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            if (conn != null)
            {
                try
                {
                    conn.Close();
                    conn = null;
                }
                catch (Exception e)
                {
                    Error = e;
                }
            }
        }
        #endregion

        #region reader
        public DbDataReader Query(string strSQL)
        {
            return GetReader(strSQL);
        }
        public DbDataReader GetReader(string strSQL, CommandType cmdtype = CommandType.Text, DbParameter[] paras = null)
        {
            if (!IsOpen) return null;
            try
            {
                using (DbCommand command = CreateCommand(strSQL, conn))
                {
                    command.CommandType = cmdtype;
                    if (paras != null)
                    {
                        command.Parameters.AddRange(paras);
                    }
                    return command.ExecuteReader(CommandBehavior.SingleResult);
                }
            }
            catch (Exception e)
            {
                Error = e;
                return null;
            }
        }
        #endregion

        public virtual Dictionary<string, string> GetColumns(string table)
        {
            var data =  conn.GetSchema(table);
            var dic = new Dictionary<string, string>();
            foreach(DataColumn col in data.Columns){
                dic.Add(col.ColumnName, col.DataType.ToString());
            }
            return dic;
        }
        #region exec

        /// <summary>
        /// 使用事务执行多条SQL
        /// </summary>
        /// <param name="SQLs"></param>
        /// <returns></returns>
        public int ExcuteSQLs(IEnumerable<string> SQLs)
        {
            if (!IsOpen) return 0;
            int result = 0;
            string sql = null;
            try
            {
                bool error = false;
                using (DbTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (DbCommand cmd = CreateCommand("", conn))
                        {
                            cmd.Transaction = transaction;
                            foreach (string SQLstr in SQLs)
                            {
                                sql = SQLstr;
                                cmd.CommandText = SQLstr;
                                result += cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        error = true;
                        Error = new Exception(sql + "\n" + e.Message);
                        transaction.Rollback();//出错，回滚
                        result = -1;
                    }
                    finally
                    {
                        if (!error)
                        {
                            transaction.Commit();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Error = new Exception(sql, e);
            }
            return result;
        }

        /// <summary>
        /// 执行单一SQL
        /// </summary>
        public int ExcuteSQL(string strSQL, CommandType cmdtype = CommandType.Text, DbParameter[] paras = null)
        {
            if (!IsOpen) return 0;
            int num = 0;
            try
            {
                using (DbCommand command = CreateCommand(strSQL, conn))
                {
                    command.CommandType = cmdtype;
                    if (paras != null)
                    {
                        command.Parameters.AddRange(paras);
                    }
                    num = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Error = new Exception(strSQL, e);
                return -1;
            }
            return num;
        }
        /// <summary>
        /// 执行SQL，并且返回第一列值
        /// </summary>
        public object ExcuteScalarSQL(string strSQL, CommandType cmdtype = CommandType.Text, DbParameter[] paras = null)
        {
            if (!IsOpen) return null;
            try
            {
                using (DbCommand command = CreateCommand(strSQL, conn))
                {
                    command.CommandType = cmdtype;
                    if (paras != null)
                    {
                        command.Parameters.AddRange(paras);
                    }
                    object obj = command.ExecuteScalar();
                    if (obj == DBNull.Value)
                    {
                        return null;
                    }
                    return obj;
                }
            }
            catch (Exception e)
            {
                Error = new Exception(strSQL, e);
                return null;
            }
        }
        #endregion
    }
}
