﻿using CommonClass.Helper;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DBHelpClass.Helper
{
    /// <summary>
    /// 数据库的通用访问代码 
    /// </summary>
    public abstract class SqlHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string connectionString = "";


        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        #region //ExecteNonQuery方法

        /// <summary>
        ///执行一个不需要返回值的SqlCommand命令，通过指定专用的连接字符串。
        /// 使用参数数组形式提供参数列表 
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQuery(string connectionString, CommandType cmdType, string cmdText,
            params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //清空SqlCommand中的参数列表
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        ///执行一个不需要返回值的SqlCommand命令，通过指定专用的连接字符串。带上事务处理
        /// 使用参数数组形式提供参数列表 
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQueryTrans(string connectionString, CommandType cmdType, string cmdText,
            params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var sqlTrans = conn.BeginTransaction();//声明事务
                //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
                PrepareCommand(cmd, conn, sqlTrans, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //清空SqlCommand中的参数列表
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        ///执行一个不需要返回值的SqlCommand命令，通过指定专用的连接字符串。
        /// 使用参数数组形式提供参数列表 
        /// </summary>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQueryTrans(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQueryTrans(connectionString, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        ///执行一个不需要返回值的SqlCommand命令，通过指定专用的连接字符串。
        /// 使用参数数组形式提供参数列表 
        /// </summary>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery(connectionString, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        ///存储过程专用
        /// </summary>
        /// <param name="cmdText">存储过程的名字</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQueryProducts(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery(CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        ///Sql语句专用
        /// </summary>
        /// <param name="cmdText">T_Sql语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public int ExecteNonQueryText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery(CommandType.Text, cmdText, commandParameters);
        }

        #endregion

        #region//GetTable方法

        /// <summary>
        /// 执行一条返回结果集的SqlCommand，通过一个已经存在的数据库连接
        /// 使用参数数组提供参数
        /// </summary>
        /// <param name="connecttionString">一个现有的数据库连接</param>
        /// <param name="cmdTye">SqlCommand命令类型</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection GetTable(string connecttionString, CommandType cmdTye, string cmdText,
            SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connecttionString))
            {
                PrepareCommand(cmd, conn, null, cmdTye, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            DataTableCollection table = ds.Tables;
            return table;
        }

        /// <summary>
        /// 执行一条返回结果集的SqlCommand，通过一个已经存在的数据库连接
        /// 使用参数数组提供参数
        /// </summary>
        /// <param name="cmdTye">SqlCommand命令类型</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection GetTable(CommandType cmdTye, string cmdText, SqlParameter[] commandParameters)
        {
            return GetTable(SqlHelper.connectionString, cmdTye, cmdText, commandParameters);
        }

        /// <summary>
        /// 存储过程专用
        /// </summary>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection GetTableProducts(string cmdText, SqlParameter[] commandParameters)
        {
            return GetTable(CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// Sql语句专用
        /// </summary>
        /// <param name="cmdText"> T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public DataTableCollection GetTableText(string cmdText, SqlParameter[] commandParameters)
        {
            return GetTable(CommandType.Text, cmdText, commandParameters);
        }

        #endregion

        /// <summary>
        /// 为执行命令准备参数
        /// </summary>
        /// <param name="cmd">SqlCommand 命令</param>
        /// <param name="conn">已经存在的数据库连接</param>
        /// <param name="trans">数据库事物处理</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">Command text，T-SQL语句 例如 Select * from Products</param>
        /// <param name="cmdParms">返回带参数的命令</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType,
            string cmdText, SqlParameter[] cmdParms)
        {
            //判断数据库连接状态
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandTimeout = 180;
            cmd.CommandText = cmdText;
            //判断是否需要事物处理
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText,
            params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            //SqlConnection conn = new SqlConnection(connectionString);
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                    return rdr;
                }
            }
            catch
            {
                //conn.Close();
                throw;
            }
        }

        #region//ExecuteDataSet方法

        /// <summary>
        /// return a dataset
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText,
            params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    return ds;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public DataSet ExecuteDataSet(CommandType cmdType, string cmdText,
            params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="cmdText">存储过程的名字</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public DataSet ExecuteDataSetProducts(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="cmdText">T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public DataSet ExecuteDataSetText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, CommandType.Text, cmdText, commandParameters);
        }

        public DataView ExecuteDataSet(string connectionString, string sortExpression, string direction,
            CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.Sort = sortExpression + " " + direction;
                    return dv;
                }
            }
            catch
            {

                throw;
            }
        }

        #endregion

        #region // ExecuteScalar方法

        /// <summary>
        /// 返回第一行的第一列
        /// </summary>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(SqlHelper.connectionString, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回第一行的第一列存储过程专用
        /// </summary>
        /// <param name="cmdText">存储过程的名字</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteScalarProducts(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(SqlHelper.connectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回第一行的第一列Sql语句专用
        /// </summary>
        /// <param name="cmdText">者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteScalarText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(SqlHelper.connectionString, CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText,
            params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText,
            params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        #endregion

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];
            if (cachedParms == null)
                return null;
            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();
            return clonedParms;
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>bool结果</returns>
        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            int cmdresult = Convert.ToInt32(ExecuteScalar(connectionString, CommandType.Text, strSql, cmdParms));
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void SqlBulkCopyByDatatable(string TableName, DataTable dt)
        {
            SqlBulkCopyByDatatable(connectionString, TableName, dt);
        }
        /// <summary>
        /// sqlbulk批量插入
        /// </summary>
        /// <param name="connectionString">目标连接字符</param>
        /// <param name="TableName">目标表</param>
        /// <param name="dt">源数据</param>
        public static void SqlBulkCopyByDatatable(string connectionString, string TableName, DataTable dt)
        {

            using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction))
            {
                try
                {
                    sqlbulkcopy.BulkCopyTimeout = 180;
                    sqlbulkcopy.DestinationTableName = TableName;
                    sqlbulkcopy.EnableStreaming = true;

                    foreach (DataColumn column in dt.Columns)
                    {
                        sqlbulkcopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                    }

                    sqlbulkcopy.WriteToServer(dt);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }

            }
        }



        public static void SqlBulkUpdateByDatatable(string connectionStr, string TableName, DataTable dt, string primaryKey, string ignoreKey = "")
        {
            //string primaryKey = "ValueID";//主键可以作为参数传进来

            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                var tempTableName = $"##Real{Guid.NewGuid().ToString("N")}";//全局临时表名。会出问题

                StringBuilder sb = new StringBuilder();
                string createString = "";
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    var columName = dataColumn.ColumnName;
                    var sqltype = GetTableColunmType(dataColumn.DataType);
                    createString += ($"[{columName}] {sqltype} ,");
                }

                string sbTempString = $@"if exists(select * from tempdb..sysobjects where id=OBJECT_ID('tempdb..{tempTableName}'))
                                        begin
                                          drop TABLE {tempTableName}
                                        end 

                                        CREATE TABLE [{tempTableName}]({createString}
                                         CONSTRAINT [PK__{tempTableName}] PRIMARY KEY CLUSTERED 
                                        (
	                                        [{primaryKey}] ASC
                                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                        ) ON [PRIMARY]

                                        if not exists(select * from dbo.SysObjects where id=OBJECT_ID(N'{TableName}'))
                                        begin
                                           CREATE TABLE [{TableName}]({createString}
                                             CONSTRAINT [PK__{TableName}] PRIMARY KEY CLUSTERED 
                                            (
	                                            [{primaryKey}] ASC
                                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                            ) ON [PRIMARY]
                                        end 
                                         ";

                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, conn, null, CommandType.Text, sbTempString, null);
                cmd.ExecuteScalar();
                cmd.Parameters.Clear();

                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connectionStr, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    try
                    {
                        #region 将数据传入临时表中作为源数据表

                        sqlbulkcopy.BulkCopyTimeout = 180;
                        sqlbulkcopy.DestinationTableName = tempTableName;
                        sqlbulkcopy.EnableStreaming = true;
                        sqlbulkcopy.WriteToServer(dt);

                        #endregion

                        string updateString = "";//拼接更新语句
                        string insertString = "";//拼接插入语句

                        foreach (DataColumn dataColumn in dt.Columns)
                        {
                            var columName = dataColumn.ColumnName;
                            if (dataColumn.DataType == typeof(System.Byte[]))
                            {
                                insertString += " null,";
                                continue;
                            }

                            if (columName != ignoreKey)
                            {
                                insertString += $" ss.{columName},";
                                updateString += $" st.{columName}=ss.{columName},";
                            }

                        }
                        updateString = updateString.TrimEnd(',');
                        insertString = insertString.TrimEnd(',');

                        //Merge临时表和目标表中数据
                        string mergeString = $@"MERGE INTO {TableName} AS st
                                                USING {tempTableName} AS ss
                                                ON st.{primaryKey} = ss.{primaryKey}
                                                WHEN MATCHED
                                                THEN UPDATE SET {updateString}
                                                WHEN NOT MATCHED BY TARGET
                                                THEN INSERT Values({insertString})
                                                WHEN NOT MATCHED BY SOURCE
                                                THEN DELETE;
                                             drop TABLE {tempTableName}
                                            ";

                        PrepareCommand(cmd, conn, null, CommandType.Text, mergeString, null);
                        int val = cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                    }
                    catch (System.Exception ex)
                    {
                        LogHelper.ErrorLog("更新数据字典失败", ex);
                        throw ex;
                    }
                }

            }

        }
        /// <summary>
        /// 更新表
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="dt"></param>
        /// <param name="primaryKey">约束键（主键或唯一键）</param>
        public static void SqlBulkUpdateByDatatable(string TableName, DataTable dt, string primaryKey, string ignoreKey = "")
        {
            SqlBulkUpdateByDatatable(connectionString, TableName, dt, primaryKey, ignoreKey);
        }



        /// <summary>
        /// 创建表----（临时表只存在于当前会话，回话结束其他地方无法访问
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tempTableName"></param>
        /// <returns></returns>
        public static bool CreateTableToSqlServer(DataTable dt, string tableName)
        {
            return CreateTableToSqlServer(connectionString, dt, tableName);
        }
        public static bool CreateTableToSqlServer(string connectionStr, DataTable dt, string tableName)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append($@"if exists(select top 1 * from sysObjects where Id=OBJECT_ID(N'{tableName}') and xtype='U')
                                        begin
                                          drop TABLE {tableName}
                                        end 
                                      CREATE TABLE {tableName}( ");

            foreach (DataColumn dataColumn in dt.Columns)
            {
                var columName = dataColumn.ColumnName;
                var sqltype = GetTableColunmType(dataColumn.DataType);
                sb.Append($"[{columName}] {sqltype} ,");
            }

            string sbTempString = sb.ToString().TrimEnd(',') + ")";
            try
            {
                ExecuteScalar(CommandType.Text, sbTempString, null);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }



        public static string GetTableColunmType(System.Type type)
        {
            var typeString = "nvarchar(255)";
            switch (type.ToString())
            {
                case "System.String":
                    break;
                case "System.Int16":
                    typeString = "int";
                    break;
                case "System.Int32":
                    typeString = "int";
                    break;
                case "System.Int64":
                    typeString = "int";
                    break;
                case "System.Decimal":
                    typeString = "decimal(18,4)";
                    break;
                case "System.Double":
                    typeString = "float";
                    break;
                case "System.DateTime":
                    typeString = "datetime";
                    break;
                case "System.Byte[]":
                    typeString = "timestamp";
                    break;
                default:
                    break;
            }

            return typeString;
        }

        public static bool CreateCurrentDataTable()
        {
            var creatSql = @"if NOT exists(select top 1 * from sysObjects where Id=OBJECT_ID(N'{tableName}') and xtype='U')
                                begin   
                                    SET ANSI_NULLS ON
                                    SET QUOTED_IDENTIFIER ON
                                    CREATE TABLE [dbo].[CurrentDayData](
	                                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                                    [ValueID] [bigint] NULL,
	                                    [Timestamp] [datetime] NOT NULL,
	                                    [RealValue] [nvarchar](100) NULL,
	                                    [Quality] [int] NULL,
	                                    [Flags] [bigint] NULL,
                                     CONSTRAINT [PK_HisValue] PRIMARY KEY NONCLUSTERED 
                                    (
	                                    [Id] ASC
                                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                                    ) ON [PRIMARY]
                                 end";

            try
            {
                ExecuteScalar(CommandType.Text, creatSql, null);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
    }
}


