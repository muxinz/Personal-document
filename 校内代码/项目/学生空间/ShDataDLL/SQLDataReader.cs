using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ShErrorDeal;

namespace ShDataDLL
{
    /// <summary>
    /// SQLDataReader分页
    /// </summary>
    public class SQLDataReader : IDisposable
    {
        SqlConnection _Conn;
        SqlDataReader _DataReader;
        DataTable _SchemaTable;
        DataSet _SchemaDataSet;
        SqlCommand _Command;
        //Stopwatch _Stopwatch = new Stopwatch();
        public static string connectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString; 
        /// <summary>
        /// Get Schema Elapse Milliseconds
        /// </summary>
        //public double GetSchemaElapsedMilliseconds
        //{
        //    get
        //    {
        //        return _Stopwatch.ElapsedMilliseconds;
        //    }
        //}

        ~SQLDataReader()
        {
            Close();
        }



        #region IDataReader Members

        /// <summary>
        /// Connect dataBase
        /// </summary>     
        public void Connect()
        {
            Close();
            _Conn = new SqlConnection(connectionString);
            _Conn.Open();
        }

        /// <summary>
        /// Close database
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Get Schema
        /// </summary>
        /// <param name="queryString">Query string</param>
        /// <returns>Schema</returns>
        public DataSet GetSchema(string queryString, params IDbDataParameter[] parameters)
        {
            //_Stopwatch.Reset();
            //_Stopwatch.Start();
            DataSet ds = new DataSet();
            ds.EnforceConstraints = false;

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand _com = new SqlCommand(queryString, _Conn);
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    SqlParameter newParam = new SqlParameter();
                    newParam.ParameterName = parameter.ParameterName;
                    newParam.DbType = parameter.DbType;
                    newParam.Value = parameter.Value;
                    _com.Parameters.Add(newParam);
                }
            }
            adapter.SelectCommand = _com;
            adapter.FillSchema(ds, SchemaType.Mapped);
            //_Stopwatch.Stop();
            return ds;
        }

        /// <summary>
        /// Open data reader for a query
        /// </summary>
        /// <param name="queryString"></param>
        public void OpenDataReader(string queryString, params IDbDataParameter[] parameters)
        {
            Connect();
            _Command = new SqlCommand(queryString, _Conn);
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    SqlParameter param2 = new SqlParameter();
                    param2.ParameterName = parameter.ParameterName;
                    param2.DbType = parameter.DbType;
                    param2.Value = parameter.Value;
                    _Command.Parameters.Add(param2);
                }
                _SchemaDataSet = GetSchema(queryString, parameters);
            }
            else
            {
                _SchemaDataSet = GetSchema(queryString, null);
            }
            _SchemaTable = _SchemaDataSet.Tables[0];
            _DataReader = _Command.ExecuteReader();
        }

        /// <summary>
        /// Close data reader for a query
        /// </summary>
        public void CloseDataReader()
        {
            try
            {
                if (!_DataReader.IsClosed)
                {
                    _Command.Cancel();
                }
            }
            catch
            {
            }
            if (_DataReader == null)
                return;
        }

        /// <summary>
        /// 获取下一行数据
        /// </summary>
        /// <returns>
        /// if reach the end of records, return null, 
        /// else return Datarow
        /// </returns>
        public DataRow NextRow()
        {
            if (_DataReader.Read())
            {
                DataRow row = _SchemaTable.NewRow();
                foreach (DataColumn col in _SchemaTable.Columns)
                {
                    row[col.ColumnName] = _DataReader[col.ColumnName];
                }

                return row;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public DataSet Query(string queryString)
        {
            return RangeQuery(queryString, 0, -1);
        }


        /// <summary>
        /// 分页查询SQL
        /// </summary>
        /// <param name="queryString">查询SQL语句</param>
        /// <param name="first">开始条数</param>
        /// <param name="last">结束条数</param>
        /// <returns>DataSet</returns>
        public DataSet RangeQuery(string queryString, long first, long last, params IDbDataParameter[] param)
        {
            try
            {
                OpenDataReader(queryString, param);

                if (first < 0)
                {
                    first = 0;
                }

                for (long i = 0; i < first; i++)
                {
                    if (!_DataReader.Read())
                    {
                        return _SchemaDataSet;
                    }
                }

                if (last < 0)
                {
                    last = 0x7FFFFFFFFFFFFFFF;
                }

                for (long i = first; i <= last; i++)
                {
                    DataRow row = NextRow();

                    if (row != null)
                    {
                        _SchemaTable.Rows.Add(row);
                    }
                    else
                    {
                        return _SchemaDataSet;
                    }
                }

                return _SchemaDataSet;
            }
            finally
            {
                CloseDataReader();
            }
        }

        #endregion

        #region IDisposable Members
        /// <summary>
        /// 释放SqlDataReader
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (_Conn != null)
                {
                    if (_Conn.State != ConnectionState.Closed &&
                        _Conn.State != ConnectionState.Broken)
                    {
                        _Conn.Close();
                    }

                    _Conn = null;
                }
            }
            catch
            {
            }
        }

        #endregion
    }
}
