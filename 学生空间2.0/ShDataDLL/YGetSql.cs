using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

//20151201-yyp  新大数据查询
//20151208-yyp  优化新大数据查询第一页数据的代码
//20151209-yyp  上次优化有误，这个改回来了，已注明原因
namespace ShDataDLL
{
    public class YGetSql
    {
        public static string GetSql(string mysql, string sortcol, int startRowIndex, int endRowIndex)
        {                          //--查询内容列,查询条件(从from开始),排序的列,  提取开始行数,     提取结束行数
            string sqlpart_s = "select * from ( ";//--提取部分的头
            string sqlorder_s = "select * ,ROW_NUMBER() OVER (" + sortcol + ") AS RowRank from( ";//--排序部分的头
            string sqlorder_e = ")Conall ";// --排序部分的尾
            string sqlpart_e = ")as Conpart where RowRank > " + startRowIndex + " AND RowRank <= " + endRowIndex + " ";// --提取部分的尾
            mysql = "select * from (" + mysql + ")Shtable";
            return sqlpart_s + sqlorder_s + mysql + sqlorder_e + sqlpart_e;
        }

        public static string Y_Deal(Dictionary<string, string> Dic_temp,List<int> temp)//string mysql, string sortcol, string mysql_tj, string tj_col, string sortcol_tj, string tablename, string tablename_col, List<int> temp)
        {                                                                              //原本的查询语句，原本的排序，查询统计列的语句，需要统计的列名，分批统计排序列，查询的表名， 列名转换的表名，   
            //string Sql_Col = Dic_temp["Sql_Col"];//不用统计的列
            string Sql_Col_TJ = Dic_temp["Sql_Col_TJ"];//需要统计的列
            //string Sql_where = Dic_temp["Sql_where"];//From语句与where条件
            //string sortcol = Dic_temp["sortcol"];//原本的语句排序
            //string sortcol_tj = Dic_temp["sortcol_tj"];//分批统计排序
            //string tj_colname = Dic_temp["tj_colname"];//需要统计的列的列名
            string tablename = Dic_temp["tablename"];//表名
            string tablename_col = Dic_temp["tablename_col"];//列名转换的表名
            
            
            int perPage = temp[0]; ;//每页数据量
            int perStatistic = temp[1]; ;//每次统计数量
            int StatisticTime = temp[2];//第几次统计
            int StatisticCount = temp[3];//总统计次数
            int NowPage = temp[4];//翻页页码，NowPage=0时，表示是查询按钮or第一行筛选or点击R；NowPage>0时,表示翻页控件点击的页码
            DataSet ds = new DataSet();

            if (NowPage == 0)//表示是查询按钮or第一行筛选or点击R
            {
                #region
                int startIndex_S = (StatisticTime - 1) * perStatistic;
                int endIndex_S = StatisticTime * perStatistic;
                if (StatisticTime == 1)//表示是第1次统计数据
                {
                    int BillCount = GetWaybillCount(Dic_temp["Sql_where"]);//运单总数
                    int pageCount = BillCount % perPage > 0 ? BillCount / perPage + 1 : BillCount / perPage;//总页数
                    int tjCount = BillCount % perStatistic > 0 ? BillCount / perStatistic + 1 : BillCount / perStatistic;//总统计次数
                    //StatisticCount = tjCount; //总统计次数=0或1时，拿第一页运单的数据

                    //此处与下面代码未整合为一处代码 是因为将一个ds中的dt放到另外一个ds时需要copy  
                    //copy运单明细数据的代价较大  这样可以节约copy数据消耗的代价
                    #region 总统计次数=0或1时，拿第一页运单的数据 最后一次统计时，拿第一页运单的数据
                    if (tjCount <= 1)// 总统计次数=0或1时 //20151214改：这样改是有理由的
                    {
                        ds = GetDetails(Dic_temp, 0, perPage);
                        if (ds != null)
                        {
                            if (ds.Tables[tablename] != null)
                            {
                                DataTable dtColMap = DataTableTransit.ChangeColName(ds.Tables[tablename], tablename_col);//变换列名返回映射表
                                ds.Tables.Add(dtColMap);
                            }
                        }
                    }
                    #endregion
                    #region 统计数据
                    if (Sql_Col_TJ.Trim().Length > 0)//有需要统计的列时
                    {
                        DataTable StatisticData = GetStatisticData(Dic_temp, startIndex_S, endIndex_S);
                        if (StatisticData != null)
                        {
                            //用于区分统计表和显示数据表的特殊列，同时存运单总数
                            StatisticData.Columns.Add("batch_statistics");
                            if (StatisticData.Rows.Count > 0)
                                StatisticData.Rows[0]["batch_statistics"] = BillCount;
                            ds.Tables.Add(StatisticData.Copy());//用copy是因为StatisticData这个表已经属于另外一个DataSet
                        }
                    }
                    else//无需要统计的列  但需要传运单总数
                    {
                        DataTable StatisticData = new DataTable();
                        StatisticData.Columns.Add("batch_statistics");
                        DataRow dr1 = StatisticData.NewRow();
                        dr1["batch_statistics"] = BillCount;
                        StatisticData.Rows.Add(dr1);
                        ds.Tables.Add(StatisticData);
                    }
                    #endregion

                    #region  总页数和总统计次数
                    DataTable dt_count = new DataTable();
                    dt_count.TableName = "Pages_Scount";
                    dt_count.Columns.Add("pagesCount");
                    dt_count.Columns.Add("StatisticCount");
                    DataRow dr = dt_count.NewRow();
                    dr["pagesCount"] = pageCount;
                    dr["StatisticCount"] = tjCount;
                    dt_count.Rows.Add(dr);
                    ds.Tables.Add(dt_count);
                    #endregion
                }
                else if (StatisticTime > 1)//表示第StatisticTime次统计数据
                {
                    #region 如果当前统计次数是最后一次统计 查询第一页运单的明细
                    if (StatisticTime == StatisticCount)
                    {
                        ds = GetDetails(Dic_temp, 0, perPage);
                        if (ds != null)
                        {
                            if (ds.Tables[tablename] != null)
                            {
                                DataTable dtColMap = DataTableTransit.ChangeColName(ds.Tables[tablename], tablename_col);//变换列名返回映射表
                                ds.Tables.Add(dtColMap);
                            }
                        }
                    }
                    #endregion
                    #region 统计数据
                    DataTable StatisticData = GetStatisticData(Dic_temp, startIndex_S, endIndex_S);
                    if (StatisticData != null)
                    {
                        //ds.Tables.Add(StatisticData);
                        StatisticData.Columns.Add("batch_statistics");
                        ds.Tables.Add(StatisticData.Copy());//用copy是因为StatisticData这个表已经属于另外一个DataSet
                    }
                    #endregion
                }
                #endregion
            }
            else//表示是分页控件翻页
            {
                #region 分页控件翻页
                int startRowIndex = (NowPage - 1) * perPage;
                int endRowIndex = NowPage * perPage;
                ds = GetDetails(Dic_temp, startRowIndex, endRowIndex);
                if (ds != null)
                {
                    if (ds.Tables[tablename] != null)
                    {

                        DataTable dtColMap =DataTableTransit.ChangeColName(ds.Tables[tablename], tablename_col);//变换列名返回映射表
                        ds.Tables.Add(dtColMap);
                    }
                }
                #endregion
            }

           XmlHelper xmlhelper = new XmlHelper();
            string retrunXML = xmlhelper.DsToXml(ds);

            return retrunXML;
        }

        public static string Y_Deal(DataTable dt, string tablename,string tablename_col,List<int> temp)
        {
            string Sql_Col = dt.Rows[0]["Sql_Col"].ToString().Trim();
            string Sql_Col_TJ = dt.Rows[0]["Sql_Col_TJ"].ToString().Trim();
            string Sql_where = dt.Rows[0]["Sql_where"].ToString().Trim();
            string sortcol = dt.Rows[0]["sortcol"].ToString().Trim();
            string sortcol_tj = dt.Rows[0]["sortcol_tj"].ToString().Trim();
            string tj_colname = dt.Rows[0]["tj_colname"].ToString().Trim();
            Dictionary<string, string> Dic_temp = new Dictionary<string, string>();
                        Dic_temp.Add("Sql_Col", Sql_Col);//不用统计的列
                        Dic_temp.Add("Sql_Col_TJ", Sql_Col_TJ);//需要统计的列
                        Dic_temp.Add("Sql_where", Sql_where);//From语句与where条件
                        Dic_temp.Add("sortcol", sortcol);//原本的语句排序
                        Dic_temp.Add("sortcol_tj", sortcol_tj);//分批统计排序
                        Dic_temp.Add("tj_colname", tj_colname);//需要统计的列的列名
                        Dic_temp.Add("tablename", tablename);//表名
                        Dic_temp.Add("tablename_col", tablename_col);//列名转换的表名
                        string retrunXML = Y_Deal(Dic_temp, temp);
            return retrunXML;
        }

        private static int GetWaybillCount(string mysql)//查询运单总数
        {
            #region
            int count = 0;
            string Procedure = "YGetBillcount";
            DataSet ds = new DataSet();//建立数据集对象

            IDataParameter[] parameters = new IDataParameter[1];
            parameters[0] = new SqlParameter("@Sql_where", SqlDbType.VarChar, -1);
            parameters[0].Value = mysql;

            ds = DbHelperSQL.RunProcedure(Procedure, parameters, "Tcount", 720);//存储过程名,存储过程参数,表名，超时时间
            if (ds != null)
            {
                if (ds.Tables["Tcount"] != null)
                {

                    count = Convert.ToInt32(ds.Tables["Tcount"].Rows[0]["sumNum"].ToString());
                }
            }
            return count;
            #endregion
        }

        private static DataTable GetStatisticData(Dictionary<string, string> Dic_temp, int startIndex_S, int endIndex_S)//统计数据
        {                                                                              //统计开始行，  统计结束行
            #region
            DataTable dt = null;
            string Procedure = "YGetStatisticData";
            DataSet ds = new DataSet();//建立数据集对象

            IDataParameter[] parameters = new IDataParameter[6];
            parameters[0] = new SqlParameter("@Sql_Col_TJ", SqlDbType.VarChar, -1);
            parameters[0].Value = Dic_temp["Sql_Col_TJ"];//需要统计的列

            parameters[1] = new SqlParameter("@Sql_where", SqlDbType.VarChar, -1);
            parameters[1].Value = Dic_temp["Sql_where"];//From语句与where条件

            parameters[2] = new SqlParameter("@tj_colname", SqlDbType.VarChar, 2000);
            parameters[2].Value = Dic_temp["tj_colname"];//需要统计的列的列名

            parameters[3] = new SqlParameter("@sortcol_tj", SqlDbType.VarChar, 500);
            parameters[3].Value = Dic_temp["sortcol_tj"];//分批统计排序

            parameters[4] = new SqlParameter("@startIndex_S", SqlDbType.Int);
            parameters[4].Value = startIndex_S;

            parameters[5] = new SqlParameter("@endIndex_S", SqlDbType.Int);
            parameters[5].Value = endIndex_S;

            ds = DbHelperSQL.RunProcedure(Procedure, parameters, "StatisticData", 720);//存储过程名,存储过程参数,表名，超时时间
            if (ds != null)
            {
                if (ds.Tables["StatisticData"] != null)
                {
                    dt = ds.Tables["StatisticData"];
                }
            }
            return dt;
            #endregion
        }

        private static DataSet GetDetails(Dictionary<string, string> Dic_temp ,int startRowIndex, int endRowIndex)//获取某一页数据
        {                                                                      //提取开始行，    提取结束行
            #region
            string Procedure = "YGetDetails";
            DataSet ds = new DataSet();//建立数据集对象
            IDataParameter[] parameters = new IDataParameter[6];
            parameters[0] = new SqlParameter("@Sql_Col", SqlDbType.VarChar, -1);
            parameters[0].Value = Dic_temp["Sql_Col"];//不用统计的列

            parameters[1] = new SqlParameter("@Sql_Col_TJ", SqlDbType.VarChar, -1);
            parameters[1].Value = Dic_temp["Sql_Col_TJ"];//需要统计的列

            parameters[2] = new SqlParameter("@Sql_where", SqlDbType.VarChar, -1);
            parameters[2].Value = Dic_temp["Sql_where"];//From语句与where条件

            parameters[3] = new SqlParameter("@sortcol", SqlDbType.VarChar, 2000);
            parameters[3].Value = Dic_temp["sortcol"];//原本的语句排序

            parameters[4] = new SqlParameter("@startRowIndex", SqlDbType.Int);
            parameters[4].Value = startRowIndex;

            parameters[5] = new SqlParameter("@endRowIndex", SqlDbType.Int);
            parameters[5].Value = endRowIndex;

            ds = DbHelperSQL.RunProcedure(Procedure, parameters, Dic_temp["tablename"], 720);//存储过程名,存储过程参数,表名，超时时间
            return ds;
            #endregion
        }
    }
}
