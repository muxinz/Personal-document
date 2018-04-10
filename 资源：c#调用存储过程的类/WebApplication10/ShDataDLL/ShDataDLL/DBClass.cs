/*作者：万三翠
开发时间：2014-12-18
功能：用了dr.read()的改过来*/
using System;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;



namespace ShDataDLL
{
    /// <summary>
    ///Class1 的摘要说明
    /// </summary>
    public class DBClass
    {
        public SqlConnection myConn = null;
        public DBClass()
        {
            //
            myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["simjx"].ConnectionString);//TODO: 在此处添加构造函数逻辑
                                                                                                            //
        }
        public DataTable GetRecords(string SqlText)
        {
            myConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(SqlText, myConn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            myConn.Close();
            return dt;
        }
        public DataSet GetDataset(List<string> sqls)
        {
            myConn.Open();
            DataSet ds1 = new DataSet();
            for (int i = 0; i < sqls.Count; i++)
            {
                string SqlText = sqls[i];
                SqlDataAdapter da = new SqlDataAdapter(SqlText, myConn);
                da.Fill(ds1, i.ToString());
            }
            myConn.Close();
            return ds1;
        }

        public int ExecuteSql(string Sql_Command)
        {
            int count;
            myConn.Open();
            SqlCommand comm = new SqlCommand(Sql_Command, myConn);
            count = comm.ExecuteNonQuery();
            myConn.Close();
            return count;
        }
        public int ExcuteSqls(List<string> sqls)
        {
            myConn.Open();
            SqlTransaction myTrans = myConn.BeginTransaction();
            SqlCommand myComm = new SqlCommand();
            myComm.Connection = myConn;
            myComm.Transaction = myTrans;
            int count = 0;
            try
            {
                for (int i = 0; i < sqls.Count; i++)
                {
                    string sql = sqls[i];
                    myComm.CommandText = sql;
                    count += myComm.ExecuteNonQuery();
                }
                myTrans.Commit();
                return count;
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                return -1;
                throw ex;

            }
            finally
            {
                myConn.Close();
            }
        }


        //柳杰： DBclass.CS加一个获取 DataSet 的方法

        public DataSet GetDataset(string[] sqls, string[] tablenames)
        {
            myConn.Open();
            DataSet ds1 = new DataSet();
            for (int i = 0; i < sqls.Length; i++)
            {
                string SqlText = sqls[i];
                SqlDataAdapter da = new SqlDataAdapter(SqlText, myConn);
                da.Fill(ds1, tablenames[i]);
            }
            myConn.Close();
            return ds1;
        }

        //public List<int> YGetTemp()//20151125-yyp  分批查询参数获取
        //{
        //    List<int> temp = new List<int> { };
        //    int perPage = Convert.ToInt32(ConfigurationManager.AppSettings["perPage"]);//每页数据量
        //    int perStatistic = Convert.ToInt32(ConfigurationManager.AppSettings["perStatistic"]);//每次统计数量
        //    int StatisticTime = 0;
        //    int StatisticCount = 0;
        //    int NowPage = 0;
        //    if (HttpContext.Current.Request.QueryString["StatisticTime"] != null)
        //    {
        //        StatisticTime = Convert.ToInt32(HttpContext.Current.Request.QueryString["StatisticTime"].ToString().Trim());
        //    }
        //    if (HttpContext.Current.Request.QueryString["StatisticCount"] != null)
        //    {
        //        StatisticCount = Convert.ToInt32(HttpContext.Current.Request.QueryString["StatisticCount"].ToString().Trim());
        //    }
        //    if (HttpContext.Current.Request.QueryString["NowPage"] != null)
        //    {
        //        NowPage = Convert.ToInt32(HttpContext.Current.Request.QueryString["NowPage"].ToString().Trim());
        //    }
        //    temp.Add(perPage);
        //    temp.Add(perStatistic);
        //    temp.Add(StatisticTime);
        //    temp.Add(StatisticCount);
        //    temp.Add(NowPage);
        //    return temp;
        //}


        //public int ExcuteSqls(List<string> sqls)
        //{
        //    myConn.Open();
        //    SqlTransaction myTrans = myConn.BeginTransaction();
        //    SqlCommand myComm = new SqlCommand();
        //    myComm.Connection = myConn;
        //    myComm.Transaction = myTrans;
        //    int count = 0;
        //    try
        //    {
        //        for (int i = 0; i < sqls.Count; i++)
        //        {
        //            string sql = sqls[i];
        //            myComm.CommandText = sql;
        //            count += myComm.ExecuteNonQuery();
        //        }
        //        myTrans.Commit();
        //        return count;
        //    }
        //    catch (Exception ex)
        //    {
        //        myTrans.Rollback();
        //        return -1;
        //        throw ex;

        //    }
        //    finally
        //    {
        //        myConn.Close();
        //    }
        //}
        //public string GetMD5(string strPwd)
        //{
        //    string a1 = "888888"; string a2 = "123456";
        //    string c1 = a1 + strPwd + a2;
        //    string pwd = "";
        //    MD5 md5 = MD5.Create();
        //    byte[] s = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(c1));
        //    s.Reverse();
        //    for (int i = 3; i < s.Length - 1; i++)
        //    {
        //        pwd = pwd + (s[i] < 198 ? s[i] + 28 : s[i]).ToString("X");
        //    }
        //    return pwd;
        //}
        //public string SelectID(string cmdText)
        //{   
        //    string uid="0";
        //    myConn.Open();
        //    SqlCommand myCmd = new SqlCommand(cmdText, myConn);
        //    SqlDataReader dr = myCmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        uid = dr["UserID"].ToString();
        //    } 
        //    myConn.Close();
        //    return uid;
        //}
        //public string NoapprovedMark(char[] up, char[] here)//上面的未审批
        //{
        //    string unlockmark = "";
        //    for (int i = 0; i < up.Length; i++)
        //    {
        //        if (here[i] == '1')//上面的是1，传的什么都变1,上面的是0，不管传的什么都是传的值本身
        //        {
        //            up[i] = '1';
        //        }
        //    }
        //    unlockmark = new string(up);
        //    return unlockmark;
        //}
        //public string YesapprovedMark(char[] up, char[] here)//上面的已审批
        //{
        //    string unlockmark = "";
        //    for (int j = 0; j < up.Length; j++)
        //    {
        //        if (here[j] == '2')//上面的是2，传什么都是变0，上面的是3，传什么就是什么，上面的是0，传什么就是什么
        //        {
        //            up[j] = '0';
        //        }
        //    }
        //    unlockmark = new string(up);
        //    return unlockmark;
        //}
        //public int ExecuteSqlBack(string Sql_Command)
        //{
        //    try
        //    {
        //        int count = 0;
        //        myConn.Open();
        //        SqlCommand comm = new SqlCommand(Sql_Command, myConn);
        //        count = comm.ExecuteNonQuery();
        //        myConn.Close();
        //        return count;
        //    }
        //    catch(Exception e)
        //    {
        //        return -1;
        //        throw e;
        //    }
        //}

        //<summary>
        //执行多条SQL语句，返回受影响的条数
        //</summary>
        //<param name="sqls">SQL语句集合</param>
        //public static int ExcuteSqls(List<string> sqls, string database)
        //{
        //    database = Application.StartupPath + "\\" + database;
        //    string connString = "Data Source=.;Initial Catalog=WuLiuServer;uid=sa;password=123456";
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            conn.Open()
        //            SqlTransaction trans = conn.BeginTransaction();
        //            cmd.Connection = conn;
        //            cmd.Transaction = trans;
        //            try
        //            {
        //                int count = 0;
        //                for (int i = 0; i < sqls.Count; i++)
        //                {
        //                    string sql = sqls[i];
        //                    cmd.CommandText = sql;
        //                    count += cmd.ExecuteNonQuery();
        //                }
        //                trans.Commit();
        //                return count;
        //            }
        //            catch (Exception ex)
        //            {
        //                trans.Rollback();
        //                return 0;
        //                throw ex;
        //            }
        //        }
        //    }
        //}
    }
}
