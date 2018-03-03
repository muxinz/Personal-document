using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ShErrorDeal;

namespace ShDataDLL
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class Conn
    {
        private SqlConnection _conn;
        private bool _connected = false;
        private IDbTransaction _transaction = null;
        private IDbCommand _transCommand = null;
        public string ConnStr = "";

        #region 王思远： 执行存储过程，返回一个 DataSet 对象 public DataSet GetDataTable(string procName, SqlParameter[] parameters)
        /// <summary>
        /// 执行存储过程，返回一个 DataTable 对象
        /// </summary>
        public DataSet GetDataSet(string procName, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(procName, _conn as SqlConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter parameter in parameters)
                {
                    adapter.SelectCommand.Parameters.Add(parameter);
                }
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                AnalysisError Analysis_Error = new AnalysisError();
                string SaveError = Analysis_Error.CatchErrorForSave(ex, procName, "Conn_GetDataSet_2");
                //保存错误信息在Logs中
                Deal_error Dealerror = new Deal_error();
                Dealerror.deal_Error(SaveError, "OtherError", "Conn_GetDataSet_2", 0);

                ds = null;
            }
            return ds;
        }
        #endregion

        //#region 小万： 执行存储过程，返回一个DataSet对象 public DataSet GetDataSet(string procName, params SqlParameter[] parameters)
        ///// <summary>
        ///// 执行存储过程，返回一个DataSet 对象
        ///// </summary>
        //public DataSet GetDataSet(string procName, params SqlParameter[] parameters)
        //{
        //    SqlDataAdapter adapter = new SqlDataAdapter(procName, _conn as SqlConnection);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    foreach (SqlParameter parameter in parameters)
        //    {
        //        adapter.SelectCommand.Parameters.Add(parameter);
        //    }
        //    DataSet ds = new DataSet();
        //    adapter.Fill(ds);
        //    return ds;
        //}
        //#endregion 


        //王思远写的方法

        public DataSet GetDataSet(List<string> sqls, List<string> Dt_Name)
        {
            string SqlText = "";
            try
            {
                DataSet ds = new DataSet();
                for (int i = 0; i < sqls.Count; i++)
                {
                    SqlText = sqls[i];
                    SqlDataAdapter da = new SqlDataAdapter(SqlText, _conn);
                    da.Fill(ds, Dt_Name[i]);
                }
                return ds;
            }
            catch (Exception ex)
            {
                AnalysisError Analysis_Error = new AnalysisError();
                string SaveError = Analysis_Error.CatchErrorForSave(ex, SqlText, "Conn_GetDataSet");
                //保存错误信息在Logs中
                Deal_error Dealerror = new Deal_error();
                Dealerror.deal_Error(SaveError, "OtherError", "Conn_GetDataSet", 0);
                return null;
            }
        }


        //小英正在用
        public DataTable Proc_GetDataTable(string sql)//这个是传递一个完整的语句，访问固定存储过程，返回一个datatable，目的是查询的不需要在语句中open key ,close key 的
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da_getall = new SqlDataAdapter("GetQueryData", _conn);
                da_getall.SelectCommand.CommandType = CommandType.StoredProcedure;
                da_getall.SelectCommand.Parameters.Add("@sqls", SqlDbType.VarChar, -1);
                da_getall.SelectCommand.Parameters["@sqls"].Value = sql;
                da_getall.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                AnalysisError Analysis_Error = new AnalysisError();
                string SaveError = Analysis_Error.CatchErrorForSave(ex, sql, "Proc_GetDataTable");
                //保存错误信息在Logs中
                Deal_error Dealerror = new Deal_error();
                Dealerror.deal_Error(SaveError, "OtherError", "Proc_GetDataTable", 0);
                return null;
            }
        }

        //小英正在用
        public DataSet Proc_GetDataSet(List<string> sql, List<string> tablename)//完整sql语句List，对应表名List，这个是传递多个查询语句以对应的表名，访问固定存储过程，返回一个dataset，目的是查询的不需要在语句中open key ,close key 的
        {
            string sqls = "";
            try
            {
                DataSet ds = new DataSet();
                for (int i = 0; i < sql.Count; i++)
                {
                    sqls = sql[i];
                    SqlDataAdapter da_getall = new SqlDataAdapter("GetQueryData", _conn);
                    da_getall.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da_getall.SelectCommand.Parameters.Add("@sqls", SqlDbType.VarChar, -1);
                    da_getall.SelectCommand.Parameters["@sqls"].Value = sqls;
                    da_getall.Fill(ds, tablename[i]);
                }
                return ds;
            }
            catch (Exception ex)
            {
                AnalysisError Analysis_Error = new AnalysisError();
                string SaveError = Analysis_Error.CatchErrorForSave(ex, sqls, "Proc_GetDataSet");
                //保存错误信息在Logs中
                Deal_error Dealerror = new Deal_error();
                Dealerror.deal_Error(SaveError, "OtherError", "Proc_GetDataSet", 0);
                return null;
            }
        }
        //小英正在用  
        #region 用存储过程执行新增、修改、上传、删除sql语句的
        public static string Exec_Sqls(string sql)//执行的完整的sql语句，返回存储过程执行的错误Number，如果返回值的长度是0表示执行成功
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
            myConn.Open();
            SqlCommand comm = new SqlCommand("sqlUp", myConn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 720;
            comm.Parameters.Add("@sqls", SqlDbType.NVarChar, -1);
            comm.Parameters["@sqls"].Value = sql;
            SqlParameter errorNum = new SqlParameter("@errorNum", SqlDbType.Int);
            errorNum.Direction = ParameterDirection.Output;   //参数类型为ReturnValue        
            comm.Parameters.Add(errorNum);
            SqlParameter errorStr = new SqlParameter("@errorStr", SqlDbType.NVarChar, -1);
            errorStr.Direction = ParameterDirection.Output;   //参数类型为ReturnValue        
            comm.Parameters.Add(errorStr);
            comm.ExecuteNonQuery();
            myConn.Close();
            string errorerrorNum = errorNum.Value.ToString().Trim();
            return errorerrorNum;
            //if (errorerrorNum.Length > 0)
            //{
            //    Response.Write("操作失败！");
            //}
            //else
            //{
            //    Response.Write("操作成功！");
            //}
        }
        #endregion

        #region 目前没有怎么用

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public Conn()
        {
            _conn = new SqlConnection(ConnectionStrings);
            openconn();
            ConnStr = ConnectionStrings;
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="connstr"></param>
        public Conn(string connstr)
        {
            _conn = new SqlConnection(connstr);
            openconn();
            ConnStr = connstr;
        }
        #endregion

        public bool Connected
        {
            get
            {
                return _connected;
            }

        }
            
        public  static readonly string CurrenConnectionStrings=ConnectionStrings;
        
        private void openconn()
        {
            if (!_connected)
            {
                try
                {
                    _conn.Open();
                    _connected = true;
                }
                catch(Exception eee)
                {
                    throw eee;
                }
            }
        }

        #region 数据库连接串 private string ConnectionStrings
        /// <summary>
        /// 数据库连接串
        /// </summary>
        private static string ConnectionStrings
        {
            get
            {

                return (string)ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

            }
        }
        #endregion

        #region 打开数据库连接 public void OpenConn()
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void OpenConn()
        {
            if (!_connected)
            {
                try
                {
                    _conn = new SqlConnection(ConnStr);
                    _conn.Open();
                    _connected = true;
                }
                catch
                {
                    throw new Exception("数据库连接失败..."); 
                }
            }
        }
        #endregion

        #region 关闭数据库连接 public void CloseConn()
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConn()
        {
            if (_connected)
            {
                try
                {
                    _conn.Close();
                    _connected = false;
                }
                catch
                {
                    throw new Exception("数据库关闭失败！");
                }
            }
        }
        #endregion

        #region 执行统计数，返回一个数值 public int ExecuteCount(string sql)
        /// <summary>
        /// 执行统计行数，返回一个数值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteCount(string sql)
        {
            SqlCommand _command = new SqlCommand(sql, _conn);
            _command.CommandTimeout = 90;
            return Convert.ToInt32(_command.ExecuteScalar());
        }
        #endregion

        #region 执行查询，返回影响行数 public int Execute(string sql)
        /// <summary>
        /// 执行查询，返回影响行数
        /// </summary>
        public int Execute(string sql)
        {
            SqlCommand _command = new SqlCommand(sql, _conn);
            _command.CommandTimeout = 90;
            try{
                return Convert.ToInt32(_command.ExecuteNonQuery());
            }
            catch(Exception eee)
            {
                throw eee;
            }
        }
        #endregion

        #region 执行查询，返回一个 DataTable对象 public DataTable GetDataTable(string sql)
        /// <summary>
        /// 执行查询，返回一个 DataTable对象
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql)
        {
                SqlCommand _command = new SqlCommand(sql, _conn);
                _command.CommandTimeout = 90;
                DataTable dt = new DataTable();
                System.Data.Common.DbDataAdapter Adapter = new System.Data.SqlClient.SqlDataAdapter(_command);
                Adapter.Fill(dt);
                return dt; 
          
        }
        #endregion

        #region 执行查询，返回一个 DataSet对象 public DataSet GetDataSet(string sql)
        /// <summary>
        ///  执行查询，返回一个 DataSet对象
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand _command = new SqlCommand(sql, _conn);
                _command.CommandTimeout = 90;
                SqlDataAdapter adapter = new SqlDataAdapter(_command);

                adapter.Fill(ds);
            }
            catch 
            {

            }
            return ds;
        }
        #endregion

        #region 执行查询，返回第一行第一列实体值 public object GetScalar(string sql)
        /// <summary>
        /// 执行查询，返回第一行第一列实体值
        /// </summary>
        public object GetScalar(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.CommandTimeout = 90;
            return cmd.ExecuteScalar();
        }
        #endregion

        #region 执行查询，返回一个 IDataReader 对象 public IDataReader GetReader(string sql)
        /// <summary>
        /// 执行查询，返回一个 SqlDataReader 对象
        /// </summary>
        public IDataReader GetReader(string sql)
        {
            IDbCommand cmd = new SqlCommand(sql, _conn);
            cmd.CommandTimeout = 90;
            return cmd.ExecuteReader();
        }
        #endregion

        #region 执行存储过程，返回影响行数 public int Execute(string procName, SqlParameter[] parameters)
        /// <summary>
        /// 执行存储过程，返回影响行数
        /// </summary>
        public int Execute(string procName, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(procName, _conn as SqlConnection);
            cmd.CommandTimeout = 90;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            return Convert.ToInt32(cmd.ExecuteNonQuery());
        }
        #endregion

        #region 执行存储过程，返回第一行第一列实体对象 public object GetScalar(string procName, SqlParameter[] parameters)
        /// <summary>
        /// 执行存储过程，返回第一行第一列实体对象
        /// </summary>
        public object GetScalar(string procName, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(procName, _conn as SqlConnection);
            cmd.CommandTimeout = 90;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            return cmd.ExecuteScalar();
        }
        #endregion

        #region 执行存储过程，返回一个 IDataReader 对象 public IDataReader GetReader(string procName, SqlParameter[] parameters)
        /// <summary>
        /// 执行存储过程，返回一个 IDataReader 对象
        /// </summary>
        public IDataReader GetReader(string procName, params SqlParameter[] parameters)
        {
            IDbCommand cmd = new SqlCommand(procName, _conn as SqlConnection);
            cmd.CommandTimeout = 90;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            return cmd.ExecuteReader();
        }
        #endregion

        #region 执行存储过程，返回一个 DataTable 对象 public DataTable GetDataTable(string procName, SqlParameter[] parameters)
        /// <summary>
        /// 执行存储过程，返回一个 DataTable 对象
        /// </summary>
        public DataTable GetDataTable(string procName, params SqlParameter[] parameters)
        {

            SqlDataAdapter adapter = new SqlDataAdapter(procName, _conn as SqlConnection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                adapter.SelectCommand.Parameters.Add(parameter);
            }
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }
        #endregion




        #region 参数化查询，执行统计数，返回一个数值 public int ExecuteCount(string sql)
        /// <summary>
        /// 执行统计行数，返回一个数值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteCount(string sql, params IDbDataParameter[] parameters)
        {
            SqlCommand _command = new SqlCommand(sql, _conn);
            _command.CommandTimeout = 90;
            for (int i = 0; i < parameters.Length; i++)
            {
                _command.Parameters.Add(parameters[i]);
            }
            int af = Convert.ToInt32(_command.ExecuteScalar());
            _command.Parameters.Clear();
            return af;
        }
        #endregion        

        #region 参数化查询，返回影响的行数 public int Execute(string commandText, params IDbDataParameter[] parameters)
        /// <summary> 
        /// 返回影响的行数，参数化查询
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int Execute(string commandText, params IDbDataParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(commandText, _conn);
            cmd.CommandTimeout = 90;
            for (int i = 0; i < parameters.Length; i++)
            {
                cmd.Parameters.Add(parameters[i]);
            }
            return cmd.ExecuteNonQuery();
        }
        #endregion

        #region 参数化查询，返回DataTable public DataTable GetDataTable(string commandText, params IDbDataParameter[] parameters)
        /// <summary>
        /// 参数化查询，返回DataTable
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string commandText, params IDbDataParameter[] parameters)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(commandText, _conn);
            foreach (IDbDataParameter parameter in parameters)
            {
                adapter.SelectCommand.Parameters.Add(parameter);
            }
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }
        #endregion

        #region 参数化查询，返回第一行第一列object对象 public object GetScalar(string commandText, params IDbDataParameter[] parameters)
        /// <summary>
        /// 参数化查询，返回第一行第一列object对象
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object GetScalar(string commandText, params IDbDataParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(commandText, _conn);
            cmd.CommandTimeout = 90;
            foreach (IDbDataParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            return cmd.ExecuteScalar();
        }
        #endregion

        #region  参数化查询，返回IDataReader public IDataReader GetReader(string commandText, params IDbDataParameter[] parameters)
        /// <summary>
        /// 参数化查询，返回IDataReader
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IDataReader GetReader(string commandText, params IDbDataParameter[] parameters)
        {
            IDbCommand cmd = new SqlCommand(commandText, _conn);
            cmd.CommandTimeout = 90;
            for (int i = 0; i < parameters.Length; i++)
            {
                cmd.Parameters.Add(parameters[i]);
            }
            return cmd.ExecuteReader();
        }
        #endregion

        #region 创建IDbDataParameter的操作
        /// <summary>
        /// 创建IDbDataParameter的操作
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(string parameterName, DbType dbType, object value)
        {
            SqlParameter retvl = new SqlParameter();
            retvl.ParameterName = parameterName;
            retvl.DbType = dbType;
            retvl.Value = value;
            return retvl;
        }
        #endregion

        #region 创建SqlParameter的操作
        /// <summary>
        /// 创建SqlParameter的操作
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public SqlParameter CreateParameter(string parameterName, SqlDbType SqldbType, object value)
        {
            SqlParameter retvl = new SqlParameter();
            retvl.ParameterName = parameterName;
            retvl.SqlDbType  = SqldbType;
            retvl.SqlValue  = value;
            return retvl;
        }
        #endregion


        #region 事务对象
        public IDbTransaction m_transaction {
            get {
                return _transaction;
            }
        }
        public IDbCommand m_transCommand
        {
            get
            {
                return _transCommand;
            }
        }
        #endregion

        #region 带事务的相关操作
        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            _transaction = _conn.BeginTransaction();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
            if (_transCommand != null)
            {
                _transCommand.Dispose();
            }
            _transaction.Dispose();
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
            if (_transCommand != null)
            {
                _transCommand.Dispose();
            }
            _transaction.Dispose();
        }

        /// <summary>
        /// 事务执行操作
        /// </summary>
        /// <param name="commandText"></param>
        public void ExecuteTransaction(string commandText)
        {
            
            if (null == _transCommand)
            {
                _transCommand = new SqlCommand(commandText, _conn);
                _transCommand.Transaction = _transaction;
            }
            else
            {
                _transCommand.CommandText = commandText;
            }
            _transCommand.ExecuteNonQuery();
        }
        /// <summary>
        /// 执行操作(参数化）
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        public void ExecuteTransaction(string commandText, params IDbDataParameter[] parameters)
        {
            if (null == _transCommand)
            {
                _transCommand = new SqlCommand(commandText, _conn);
                _transCommand.Transaction = _transaction;
            }
            else
            {
                _transCommand.CommandText = commandText;
            }

            _transCommand.Parameters.Clear();
            for (int i = 0; i < parameters.Length; i++)
            {
                _transCommand.Parameters.Add(parameters[i]);
            }
            _transCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行操作 返回第一行第一列
        /// </summary>
        /// <param name="commandText"></param>
        public object ExecuteTransactionScalar(string commandText){
            if (null == _transCommand)
            {
                _transCommand = new SqlCommand(commandText, _conn);
                _transCommand.Transaction = _transaction;
            }
            else
            {
                _transCommand.CommandText = commandText;
            }
            return _transCommand.ExecuteScalar();
        }
        /// <summary>
        /// 执行操作 返回第一行第一列(参数化）
        /// </summary>
        /// <param name="commandText"></param>
        public object ExecuteTransactionScalar(string commandText, params IDbDataParameter[] parameters)
        {
            if (null == _transCommand)
            {
                _transCommand = new SqlCommand(commandText, _conn);
                _transCommand.Transaction = _transaction;
            }
            else
            {
                _transCommand.CommandText = commandText;
            }

            _transCommand.Parameters.Clear();
            for (int i = 0; i < parameters.Length; i++)
            {
                _transCommand.Parameters.Add(parameters[i]);
            }
            return _transCommand.ExecuteScalar();
        }

        public DataTable getDataTableCommand(string sql)
        {
            if (null == _transCommand)
            {
                _transCommand = new SqlCommand(sql, _conn);
                _transCommand.Transaction = _transaction;
            }
            else
            {
                _transCommand.CommandText = sql;
 
            }
            DataTable dt = new DataTable();
            System.Data.Common.DbDataAdapter Adapter = new System.Data.SqlClient.SqlDataAdapter((SqlCommand)_transCommand);
            Adapter.Fill(dt);
            return dt; 
        }
        #endregion
        #endregion

    }
}
