using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace wuliu
{
    class ReadXml
    {

       public  string NowXmlBase = Application.StartupPath + Login_user.Now_WareHouse + "SystemParameterAdd.xml"; //Application.StartupPath + "\\Data\\SystemParameterAdd.xml";
       public  string DataXmlBase = Application.StartupPath + "\\Data\\SystemParameterAdd.xml";
       public  string XmlBase_global = Application.StartupPath + "\\SystemParameterAdd.xml";
       public  string NowXmlBase_set = Application.StartupPath + Login_user.Now_WareHouse + "SystemParameterSet.xml";//Application.StartupPath + "\\Data\\SystemParameterSet.xml";
       public  string DataXmlBase_set = Application.StartupPath + "\\Data\\SystemParameterSet.xml";
       public  string DataXmlBase_batch = Application.StartupPath + "\\SystemBatchQuery.xml";


        #region 读取xml字符串//重要
        public string Read_Messagetext(string  xml_base, string nodename)//读取xml中的内容
        {
            string  Messagetext ="";
            XmlDocument xd = new XmlDocument();//创建文档
            if (System.IO.File.Exists(xml_base))//查看文件是否存在
            {
                xd.Load(xml_base);//加载文档
                XmlNodeList xxlist = xd.GetElementsByTagName("SystemParameter");//所有节点
                foreach (XmlNode xxNode in xxlist)//单个节点
                {
                    XmlNodeList xxlist1 = xxNode.ChildNodes;//获取子节点
                    foreach (XmlNode xxNode1 in xxlist1)
                    {
                        XmlNodeList xxlist2 = xxNode1.ChildNodes;//获取子节点
                        foreach (XmlNode xxNode2 in xxlist2)
                        {
                            if (xxNode2.Name == nodename)
                            {
                                Messagetext = xxNode2.InnerText.Trim();
                                break;
                            }
                        }
                    }
                }
            }
            return Messagetext;
        }
        #endregion

        #region 保存XML//重要
        public void Save_Messagetext(string xml_base,string messagetext, string nodename)
        {
            XmlDocument xd = new XmlDocument();//创建文档
            if (System.IO.File.Exists(xml_base))
            {
                xd.Load(xml_base);//加载文档
                XmlNodeList xxlist = xd.GetElementsByTagName("SystemParameter");//所有节点
                foreach (XmlNode xxNode in xxlist)//单个节点
                {
                    XmlNodeList xxlist1 = xxNode.ChildNodes;//获取子节点
                    foreach (XmlNode xxNode1 in xxlist1)
                    {
                        XmlNodeList xxlist2 = xxNode1.ChildNodes;//获取子节点
                        foreach (XmlNode xxNode2 in xxlist2)
                        {
                            if (xxNode2.Name == nodename)
                            {
                                xxNode2.InnerText = messagetext.Trim();
                            }
                        }
                    }
                }
                xd.Save(xml_base);
            }
        }
        #endregion


        public static string dealErrorXml(string xmlstr)
        {
            string errortip = "";
            if (xmlstr.Length > 0 && xmlstr.IndexOf("<ErrorMessage>") >= 0)
            {

                XmlDocument xd = new XmlDocument();//创建文档

                xd.LoadXml(xmlstr); ;//加载文档
                XmlNode errorState = xd.SelectSingleNode("/ErrorMessage/ErrorStatus");
                string errorNum = errorState.InnerText.Trim();
                XmlNode errorDescrip = xd.SelectSingleNode("/ErrorMessage/ErrorDescrip");
                string descriptip = errorDescrip.InnerText.Trim();
                if (errorNum.Length == 0)
                {
                    errortip = "错误原因：" + descriptip;
                }
                else
                {
                    errortip = "错误原因：" + errorNum + "\n错误描述：" + descriptip;
                }
            }
            return errortip;
        }


        /// <summary>
        /// 读取排序规则
        /// </summary>
        /// <param name="strpath"></param>文件的绝对路径
        /// <param name="node"></param>打印类型的节点
        /// <param name="sortName"></param>排序的列名
        /// <param name="sortOrder"></param>排序（升降）
        public void ReadNode(string strpath, string node, out string sortName, out string sortOrder)
        {
            sortName = "";
            sortOrder = "";
            if (System.IO.File.Exists(strpath))
            {
                XmlDocument xd = new XmlDocument();//创建文
                System.IO.StreamReader sr = new System.IO.StreamReader(strpath);
                string strXml = sr.ReadToEnd().Trim();
                sr.Close();
                if (strXml.Length == 0)
                {
                    return;
                }
                xd.Load(strpath);//加载文档

                XmlNode xmlNode = xd.SelectSingleNode("//" + node);
                if (xmlNode != null)
                {
                    sortName = xmlNode.InnerText.Trim();

                    XmlElement xmlele = (XmlElement)xmlNode;
                    sortOrder = xmlele.GetAttribute("sortOrder");
                }
            }
        }

        /// <summary>
        /// 保存排序规则
        /// </summary>
        /// <param name="strpath"></param>文件绝对路径
        /// <param name="className"></param>根节点
        /// <param name="node"></param>打印类型的节点
        /// <param name="sortName"></param>排序的列名
        /// <param name="sortOrder"></param>排序（升降）
        public void SaveNode(string strpath, string className, string node, string sortName, string sortOrder)
        {
            XmlDocument xd = new XmlDocument();//创建文

            if (System.IO.File.Exists(strpath))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(strpath);
                string strXml = sr.ReadToEnd().Trim();
                sr.Close();
                if (strXml.Length > 0)
                {
                    xd.Load(strpath);//加载文档
                    XmlNode xmlNode = xd.SelectSingleNode("//" + node);
                    if (xmlNode != null)
                    {
                        xmlNode.InnerText = sortName.Trim();

                        XmlElement xmlele = (XmlElement)xmlNode;
                        xmlele.SetAttribute("sortOrder", sortOrder);
                    }
                    else
                    {
                        XmlNode root1 = xd.DocumentElement;
                        System.Xml.XmlElement ele2 = xd.CreateElement(node);
                        ele2.InnerText = sortName.Trim();
                        ele2.SetAttribute("sortOrder", sortOrder);
                        root1.AppendChild(ele2);
                    }
                }
                else
                {

                    System.Xml.XmlDeclaration dec = xd.CreateXmlDeclaration("1.0", "utf-8", null);
                    xd.AppendChild(dec);
                    System.Xml.XmlElement ele = xd.CreateElement(className);
                    ele.SetAttribute("Version", "1");
                    xd.AppendChild(ele);

                    System.Xml.XmlElement ele2 = xd.CreateElement(node);
                    ele2.InnerText = sortName.Trim();
                    ele2.SetAttribute("sortOrder", sortOrder);

                    ele.AppendChild(ele2);
                }
            }
            else
            {
                System.Xml.XmlDeclaration dec = xd.CreateXmlDeclaration("1.0", "utf-8", null);
                xd.AppendChild(dec);
                System.Xml.XmlElement ele = xd.CreateElement(className);
                ele.SetAttribute("Version", "1");
                xd.AppendChild(ele);

                System.Xml.XmlElement ele2 = xd.CreateElement(node);
                ele2.InnerText = sortName.Trim();
                ele2.SetAttribute("sortOrder", sortOrder);

                ele.AppendChild(ele2);
            }

            string filename = strpath;
            string hh = (xd.InnerXml.ToString());
            System.IO.FileStream myFs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
            System.IO.StreamWriter mySw = new System.IO.StreamWriter(myFs);
            mySw.Write(hh);
            mySw.Close();
            myFs.Close();
        }


        #region 读取xml字符串//重要
        public string Read_SingleNode(string xml_base, string nodename)//读取xml中的内容
        {
            string Messagetext = "";
            XmlDocument xd = new XmlDocument();//创建文档
            if (System.IO.File.Exists(xml_base))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(xml_base);
                string strXml = sr.ReadToEnd().Trim();
                sr.Close();
                if (strXml.Length > 0)
                {
                    xd.Load(xml_base);//加载文档
                    XmlNode xmlNode = xd.SelectSingleNode("//" + nodename);
                    if (xmlNode != null)
                    {
                        Messagetext = xmlNode.InnerText.Trim();
                    }
                }
            }
            return Messagetext;
        }
        #endregion
    
    }
}
