using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using ShErrorDeal;

namespace ShDataDLL
{
    public class XmlHelper
    {
        private XmlDocument _xml;
        /// <summary>
        /// 万三翠用
        /// </summary>
        /// <param name="xmlstr"></param>
        /// <param name="rootnode"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetxmlText(string xmlstr, string rootnode)//用这个方法节点不能有重复，还有一个节点一个参数，有属性值的不适用
        {
            Dictionary<string, string> txtDic = new Dictionary<string, string>();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlstr);
            XmlNode root = xml.SelectSingleNode(rootnode);
            XmlNodeList nodelist = root.ChildNodes;
            for (int i = 0; i < nodelist.Count; i++)
            {
                XmlNode xmlnode = nodelist[i];
                string nodename = xmlnode.Name.ToString().Trim();
                string nodevalue = xmlnode.InnerText.ToString().Trim();
                txtDic.Add(nodename, nodevalue);
            }
            return txtDic;
        }
        
        //周海梅：找到指定节点的值并返回给调用者
        public string GetInnerText(string strXml, string parentNode, string xmlNode) //strXml:xml字符串//parentNode:父节点或者需要查找的大节点//xmlNode:需要找到的节点
        {
            string nodeText = "";

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(strXml);
                XmlNode root = xml.SelectSingleNode(parentNode);
                XmlNodeList nodelist = root.ChildNodes;
                foreach (XmlNode node in nodelist)
                {
                    if (node.Name == xmlNode)
                    {
                        XmlElement ele = (XmlElement)node;
                        nodeText = ele.InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                AnalysisError Analysis_Error = new AnalysisError();
                string SaveError = Analysis_Error.CatchErrorForSave(ex, strXml, "xmlhelper_GetInnerText");
                //保存错误信息在Logs中
                Deal_error Dealerror = new Deal_error();
                Dealerror.deal_Error(SaveError, "OtherError", "xmlhelper_GetInnerText", 0);
            }
            return nodeText;
        }

        #region 以下是在原来的基础上新加的方法DataTableToXml(),DataSeTToXml(),XmlToDataSet(),XmlToDataTable()
        /// <summary>
        /// DataTable转换成xml
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string DtToXml(DataTable dt)
        {
            try
            {
                System.IO.TextWriter tw = new System.IO.StringWriter();
                if (dt!=null)//2015-12-04加是否为null判断
                {
                    dt.TableName = dt.TableName.Length == 0 ? "Table1" : dt.TableName;
                    dt.WriteXml(tw);
                    dt.WriteXmlSchema(tw);
                }
                return tw.ToString();
            }
            catch (Exception ex)
            {
                AnalysisError Analysis_Error = new AnalysisError();
                string SaveError = Analysis_Error.CatchErrorForSave(ex, "无", "xmlhelper_DtToXml");
                //保存错误信息在Logs中
                Deal_error Dealerror = new Deal_error();
                Dealerror.deal_Error(SaveError, "OtherError", "xmlhelper_DtToXml", 0);

                //错误信息返回本地
                string SendError = Analysis_Error.CatchErrorForClient(ex);

                return SendError;
            }

        }
        /// <summary>
        /// DataSet转换成xml
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public string DsToXml(DataSet ds)
        {
            try
            {
                System.IO.TextWriter tw = new System.IO.StringWriter();
                if (ds != null)//2015-12-04加是否为null判断
                {
                    ds.WriteXml(tw);
                    ds.WriteXmlSchema(tw);
                }

                return tw.ToString();
            }
            catch (Exception ex)
            {
                AnalysisError Analysis_Error = new AnalysisError();
                string SaveError = Analysis_Error.CatchErrorForSave(ex, "无", "xmlhelper_DsToXml");
                //保存错误信息在Logs中
                Deal_error Dealerror = new Deal_error();
                Dealerror.deal_Error(SaveError, "OtherError", "xmlhelper_DsToXml", 0);

                //错误信息返回本地
                string SendError = Analysis_Error.CatchErrorForClient(ex);

                return SendError;
            }

        }
        /// <summary>
        /// xml转换成DataSet
        /// </summary>
        /// <param name="XmlStr"></param>
        /// <returns></returns>
        public DataSet XmlToDs(string XmlStr)
        {
            DataSet ds_temp = new DataSet();
            try
            {

                System.IO.TextReader str_ds = new System.IO.StringReader(XmlStr.Substring(0, XmlStr.IndexOf("<?xml")));
                System.IO.TextReader trSchema = new System.IO.StringReader(XmlStr.Substring(XmlStr.IndexOf("<?xml")));

                ds_temp.ReadXmlSchema(trSchema);
                ds_temp.ReadXml(str_ds);

            }
            catch //(Exception ex)
            {
                ds_temp = null;
            }
            return ds_temp;

        }
        /// <summary>
        /// xml转换成表
        /// </summary>
        /// <param name="XmlStr"></param>
        /// <returns></returns>
        public DataTable XmlToDt(string XmlStr)
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

        #region 目前还没有人用到

        #region 构造函数 Xml操作助手 public XmlHelper()
        /// <summary>
        /// Xml操作助手 
        /// </summary>
        public XmlHelper()
        {
            _xml = new XmlDocument();
        }
        #endregion

        #region 加载指定（物理）路径的XML文档 public virtual void Load(string path)
        /// <summary>
        /// 加载指定（物理）路径的XML文档
        /// </summary>
        /// <param name="path"></param>
        public void Load(string path){
            try
            {
                if (_xml != null)
                {
                    _xml = new XmlDocument();
                }
                _xml.Load(path);
            }
            catch (Exception e) {
                throw new Exception(e.Message.ToString());
            }
        }
        #endregion

        #region 加载指定（物理）路径的XML文档，返回DataSet public DataSet GetDataSet(string path)
        /// <summary>
        /// 加载指定（物理）路径的XML文档，返回DataSet
        /// </summary>
        /// <param name="path"></param>
        public DataSet GetDataSet(string path)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(path);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
        #endregion

        #region 获取XML为DataSet，返回DataSet public DataSet GetDataSet()
        /// <summary>
        /// 获取XML为DataSet，返回DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet()
        {
            XmlReader xmlreader = XmlReader.Create(new System.IO.StringReader(_xml.OuterXml));
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(xmlreader);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
        #endregion

        #region 加载字符串到跟接点  public void SetRootXml(string content)
        /// <summary>
        /// 加载字符串到跟接点
        /// </summary>
        /// <param name="content"></param>
        public void SetRootXml(string content)
        {
            _xml.DocumentElement.InnerXml = content;
        }
        #endregion

        #region 创建Xml文档 public XmlDocument CreateXml(string version, string encoding, string standalone, string rootname)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="version">版本</param>
        /// <param name="encoding">编码</param>
        /// <param name="standalone">“yes”或“no”</param>
        /// <param name="rootname">根接点名</param>
        /// <returns></returns>
        public XmlDocument CreateXml(string version, string encoding, string standalone, string rootname)
        {
            XmlNode m_root = _xml.DocumentElement;
            _xml.AppendChild(_xml.CreateXmlDeclaration(version, encoding, standalone));
            _xml.AppendChild(_xml.CreateElement(rootname));
            return _xml;
        }
        #endregion

        #region 获取Xml文档 public XmlDocument GetXml()
        /// <summary>
        /// 获取Xml文档
        /// </summary>
        /// <returns>返回XmlDocument</returns>

        public XmlDocument GetXml() {
            return _xml;
        }
        #endregion

        #region 获取根接点下的字符串 public string GetString()
        /// <summary>
        /// 获取根接点下的字符串
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            XmlNode m_root = _xml.DocumentElement;
            return m_root.InnerXml;
        }
        #endregion

        #region 获取指定接点列表 public virtual XmlNodeList GetNodesList(string nodepath)
        /// <summary>
        /// 获取指定接点列表
        /// </summary>
        /// <param name="nodepath">接点路径</param>
        /// <returns>XmlNodeList</returns>
        public virtual XmlNodeList GetNodesList(string nodepath)
        {
            XmlNodeList xmlnode = _xml.SelectNodes(nodepath);
            return xmlnode;
        }
        #endregion

        #region 获取单个接点 public virtual XmlNode GetNode(string nodepath)
        /// <summary>
        /// 获取单个接点
        /// </summary>
        /// <param name="nodepath">接点路径</param>
        /// <returns>XmlNode</returns>
        public virtual XmlNode GetNode(string nodepath)
        {
            XmlNode xmlnode = _xml.SelectSingleNode(nodepath);
            return xmlnode;
        }
        #endregion

        #region 获取单个接点的值 public virtual string GetNodeText(string nodepath)
        /// <summary>
        /// 获取单个接点的值 
        /// </summary>
        /// <param name="nodepath">接点路径</param>
        /// <returns>null or string</returns>
        public virtual string GetNodeText(string nodepath)
        {
            XmlNode xmlnode = _xml.SelectSingleNode(nodepath);
            if (xmlnode == null)
            {
                return null;
            }
            else
            {
                return xmlnode.InnerText.ToString();
            }
        }
        #endregion

        #region 获取接点属性 public virtual string GetNodeKey(string nodepath, string key)
        /// <summary>
        /// 获取接点属性
        /// </summary>
        /// <param name="nodepath">接点路径</param>
        /// <param name="key">属性</param>
        /// <returns>null or string</returns>
        public virtual string GetNodeKey(string nodepath, string key)
        {
            XmlNode xmlnode = _xml.SelectSingleNode(nodepath);
            if (xmlnode == null)
            {
                return null;
            }
            else
            {
                XmlAttribute xmlnodekey = xmlnode.Attributes[key];
                if (xmlnodekey == null)
                {
                    return null;
                }
                else
                {
                    return xmlnodekey.Value.ToString();
                }
            }
        }
        #endregion

        #region 设置接点的值 public void SetNodeText(string NodeName, string NodeText)
        public void SetNodeText(string NodeName, string NodeText)
        {
            XmlNode mynode = _xml.SelectSingleNode(NodeName);
            mynode.InnerText = NodeText;
        }
        #endregion

        #region 添加接点 public bool AddNode(string key, string value)
        /// <summary>
        /// 添加接点
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns>bool</returns>
        public bool AddNode(string key, string value)
        {

            XmlNode m_root = _xml.DocumentElement;
            if (m_root == null)
            {
                return false;
            }

            XmlElement _ItemName = _xml.CreateElement(key);
            _ItemName.InnerXml = value;
            m_root.AppendChild(_ItemName);
            return true;
        }
        #endregion

        #region 保存XML 到硬盘  public bool Save()
        /// <summary>
        /// 保存XML 到硬盘
        /// </summary>
        /// <param name="path">物理路径</param>
        /// <returns></returns>
        public bool Save(string path) {
            _xml.Save(path);
            return true;
        }
        #endregion

        #endregion

    }
}
