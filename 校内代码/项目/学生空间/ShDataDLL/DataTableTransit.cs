using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;


namespace ShDataDLL
{
    /// <summary>
    ///DataTableTransit 的摘要说明
    /// </summary>
    public class DataTableTransit
    {
        public DataTableTransit()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }


        #region 列名处理
        /*
     * 小于52列可按字母顺序转换为一位
     * 大于52列时按字母加数字转换为两位
     * 大于572列之后不做转换
     */

        /// <summary>
        /// 简写数据表列名返回映射关系表，dtSource数据表，dtColMapName映射关系表名
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="dtColMapName"></param>
        /// <returns></returns>
        public static DataTable ChangeColName(DataTable dtSource, string dtColMapName)
        {
            DataTable dtColMap = new DataTable();
            dtColMap.Columns.Add("S");//源列名
            dtColMap.Columns.Add("T");//目标列名
            dtColMap.TableName = dtColMapName;
            if (dtSource == null)
            {
                return dtColMap;
            }
            char TagStart = 'A';
            //char Tag = 'A';
            for (int i = 0; i < dtSource.Columns.Count; i++)
            {
                string colname = "";
                if (i < 26)//大写一个字符可转换26列
                {
                    colname = ((char)((int)TagStart + i)).ToString().Trim();
                }
                else if (i < 52)//小写一个字符 可转换52列
                {
                    colname = ((char)((int)TagStart + 6 + i)).ToString().Trim();
                }
                else
                {
                    int x = (i - 52) % 10;
                    int y = (i - 52) / 10;
                    if (y < 26)//2位可转换260列
                    {
                        colname = ((char)((int)TagStart + y)).ToString().Trim() + x.ToString().Trim();
                    }
                    else if (y < 52)//2位可转换520列
                    {
                        colname = ((char)((int)TagStart + 6 + y)).ToString().Trim() + x.ToString().Trim();
                    }
                    else
                    {
                        colname = "";
                    }


                }

                if (colname.Trim().Length > 0)
                {
                    dtColMap.Rows.Add(colname, dtSource.Columns[i].ColumnName);
                    dtSource.Columns[i].ColumnName = colname;

                }
            }
            return dtColMap;
        }
        #endregion



    }
}
