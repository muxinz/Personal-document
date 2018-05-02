using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.Collections.Generic;

namespace Sh_Common_DLL
{
    /// <summary>
    ///XmlHelper 的摘要说明
    /// </summary>
    public class XmlHelper
    {
        public XmlHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public static  string DataTableToXml(DataTable dt)
        {
            System.IO.TextWriter tw = new System.IO.StringWriter();

            dt.TableName = dt.TableName.Length == 0 ? "Table1" : dt.TableName;
            dt.WriteXml(tw);
            dt.WriteXmlSchema(tw);

            return tw.ToString();
        }
        public static string DataSeTToXml(DataSet ds)
        {
            System.IO.TextWriter tw = new System.IO.StringWriter();
            ds.WriteXml(tw);
            ds.WriteXmlSchema(tw);

            return tw.ToString();
        }
        public static DataSet XmlToDataSet(string XmlStr)
        {
            DataSet ds_temp = new DataSet();
            try
            {

                System.IO.TextReader str_ds = new System.IO.StringReader(XmlStr.Substring(0, XmlStr.IndexOf("<?xml")));
                System.IO.TextReader trSchema = new System.IO.StringReader(XmlStr.Substring(XmlStr.IndexOf("<?xml")));

                ds_temp.ReadXmlSchema(trSchema);
                ds_temp.ReadXml(str_ds);



            }
            catch (Exception ex)
            {
                ds_temp = null;
            }
            return ds_temp;

        }

        #region xml转换为DataTable
        public static DataTable XmlToDataTable(string XmlStr)
        {
            DataTable dtReturn = new DataTable();
            try
            {
                System.IO.TextReader trDataTable = new System.IO.StringReader(XmlStr.Substring(0, XmlStr.IndexOf("<?xml")));
                System.IO.TextReader trSchema = new System.IO.StringReader(XmlStr.Substring(XmlStr.IndexOf("<?xml")));

                dtReturn.ReadXmlSchema(trSchema);
                dtReturn.ReadXml(trDataTable);
            }
            catch
            {
                dtReturn = null;
            }

            return dtReturn;
        }
        #endregion

        /// <summary>
        /// 传入俩个List 一个为xml节点，一个为节点值，最后返回生成的xml
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="nodeVale"></param>
        /// <returns></returns>
        public static string ListToXml(List<string> xmlNode, List<string> nodeVale)
        {
            string strXml = "";

            XmlDocument xml = new XmlDocument();
            System.Xml.XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", "utf-8", null);
            xml.AppendChild(dec);
            System.Xml.XmlElement ele = xml.CreateElement("NEWDATASET");
            for (int i = 0; i < xmlNode.Count; i++)
            {
                XmlElement _ele = xml.CreateElement(xmlNode[i]);
                _ele.InnerText = nodeVale[i];
                ele.AppendChild(_ele);
            }
            xml.AppendChild(ele);

            return strXml = xml.InnerXml.ToString() ;

        }

    }
}
