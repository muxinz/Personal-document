using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ShUpLoad_dll
{
   public  class Sh_ReadXml
    {

        public string NowXmlBase = "";// Application.StartupPath + Login_user.Now_WareHouse + "SystemParameterAdd.xml"; //正在使用的
        public string XmlBase_set = "";// Application.StartupPath + Login_user.Now_WareHouse + "SystemParameterSet.xml";//正在使用的

        public string DataXmlBase = "";// Application.StartupPath + "\\Data\\SystemParameterAdd.xml";
        public string DataXmlBase_set = "";// Application.StartupPath + "\\Data\\SystemParameterSet.xml";
        public string XmlBase_global = "";// Application.StartupPath + "\\SystemParameterAdd.xml";
        //public static string Data_errorcode = "";
        public string SystemBatchQuery_xml = "";//SystemBatchQuery.xml
        public Sh_ReadXml(string _Application_Startup, string _Login_user_Data_Name, string _Login_user_Now_WareHouse)//whid是主货站好，whid_new是异地代办货站号
        {
           string  Application_Startup = Sh_ApplicationStartup.Application_Startup(_Application_Startup);
            if (_Login_user_Data_Name != _Login_user_Now_WareHouse)
            {
                NowXmlBase = Application_Startup + _Login_user_Now_WareHouse + "SystemParameterAdd.xml";
                XmlBase_set = Application_Startup + _Login_user_Now_WareHouse + "SystemParameterSet.xml";
            }
            else
            {
                NowXmlBase = Application_Startup + _Login_user_Data_Name + "SystemParameterAdd.xml";
                XmlBase_set = Application_Startup + _Login_user_Data_Name + "SystemParameterSet.xml";
            }
            DataXmlBase = Application_Startup + _Login_user_Data_Name + "SystemParameterAdd.xml";
            DataXmlBase_set = Application_Startup + _Login_user_Data_Name + "SystemParameterSet.xml";
            XmlBase_global = Application_Startup + "\\SystemParameterAdd.xml";
            //Data_errorcode = Application_Startup + "ErrorCode_Description.xml";
            SystemBatchQuery_xml = Application_Startup + "\\SystemBatchQuery.xml";//分批查询对应的某功能最大查询数量，以及每页查询多少
        }

        #region 读取xml字符串
        public string Read_Messagetext(string xml_base, string nodename)//读取xml中的内容
        {
            string Messagetext = "";
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
                                Messagetext = xxNode2.InnerText.Trim();
                                break;
                                {
                                    Messagetext = xxNode2.InnerText;
                                }
                            }
                        }
                    }
                }
                return Messagetext;
            }
        }
        #endregion

        #region 保存XML
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


        //public static string dealErrorXml(string xmlstr)
        //{
        //    string errortip = "";
        //    if (xmlstr.Length > 0 && xmlstr.IndexOf("<ErrorMessage>") >= 0)
        //    {

        //        XmlDocument xd = new XmlDocument();//创建文档

        //        xd.LoadXml(xmlstr); ;//加载文档
        //        XmlNode errorState = xd.SelectSingleNode("/ErrorMessage/ErrorStatus");
        //        string errorNum = errorState.InnerText.Trim();
        //        XmlNode errorDescrip = xd.SelectSingleNode("/ErrorMessage/ErrorDescrip");
        //        string descriptip = errorDescrip.InnerText.Trim();
        //        if (errorNum.Length == 0)
        //        {
        //            errortip = "错误原因：" + descriptip;
        //        }
        //        else
        //        {
        //            errortip = "错误原因：" + errorNum + "\n错误描述：" + descriptip;
        //        }
        //    }
        //    return errortip;
        //}

        public static string DealUnknownWrong(string xmlstr, string selferrorcode, int flag)//flag=0代表不使用自己的提示，flag=1代表使用自己的提示
        {
            string messageShow = string.Empty;
            if (xmlstr.IndexOf("<ErrorMessage") > -1)
                messageShow = Handle_WebReturn(xmlstr, selferrorcode, flag);
            else if (xmlstr.IndexOf("<URLWRONG>") > -1)
                messageShow = Handle_UrlWrong(xmlstr, selferrorcode, flag);
            else
            {
                if (flag == 0)
                    messageShow = "由于网络原因产生未知错误！";
                else
                    messageShow = selferrorcode;
            }
            return messageShow;
        }



        public static string Handle_UrlWrong(string xmlstr, string selferrorcode, int flag)
        {
            string reStr = string.Empty;
            if (xmlstr.Length > 0 && xmlstr.IndexOf("<URLWRONG>") >= 0)
            {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(xmlstr);
                XmlNode node = xd.SelectSingleNode("URLWRONG");
                XmlElement xe = (XmlElement)node;
                string number = xe.InnerText.ToString();
                Regex regex = new Regex("^[0-9]*$");  //验证数数字，有可能是节点错误，那么拿下来的还是字符串
                if (regex.IsMatch(number))
                    reStr = "错误编号：" + number + "\n错误描述：由于网络原因导致数据传递出错！";
                else
                {
                    if (flag == 0)
                        reStr = "由于网络原因产生未知错误！";
                    else
                        reStr = selferrorcode;
                }
            }
            else
            {
                if (flag == 0)
                    reStr = "由于网络原因产生未知错误！";
                else
                    reStr = selferrorcode;
            }
            return reStr;
        }

 
      
       
       #region 解析服务返回的错误的xml格式的字符串并给用户一个可以理解的错误提示
        public static string errorNumCode = "";//xml格式的错误解析完成之后调用者还是需要状态码的变量
        public static string dealErrorXml(string xmlstr)
        {

            string errortip = "";
            if (xmlstr.Length > 0 && xmlstr.IndexOf("<ErrorMessage") >= 0)
            {

                XmlDocument xd = new XmlDocument();//创建文档

                xd.LoadXml(xmlstr); //加载文档
                XmlNode errorState = xd.SelectSingleNode("/ErrorMessage/ErrorStatus");
                XmlNode errorDescrip = xd.SelectSingleNode("/ErrorMessage/ErrorDescrip");
                if (errorState != null && errorDescrip != null)
                {
                    string errorNum = errorState.InnerText.Trim().ToLower();

                    string descriptip = errorDescrip.InnerText.Trim();

                    #region 根路径存储xml的做法
                    XmlNode root = xd.SelectSingleNode("/ErrorMessage");
                    if (root.Attributes["sty"] != null) 
                    {
                        string rootsty = root.Attributes["sty"].Value.ToString(); ;
                        List<string> code_descrip = new List<string>();
                        code_descrip = dealCodeXml(rootsty, errorNum, descriptip);
                        if (code_descrip.Count > 0)
                        {
                            errortip = code_descrip[1].ToString();
                            errorNumCode = code_descrip[0].ToString();
                        }
                        else
                        {
                            errorNumCode = "NotFound";
                            errortip = "错误原因：" + errorNumCode + "\n错误描述：" + descriptip;
                        }
                    }
                    else
                    {
                        errorNumCode = "NotFound";
                        errortip = "错误原因：" + errorNumCode + "\n错误描述：" + descriptip;
                    }
                    #endregion
                }
                else
                {
                    errorNumCode = "NotFound";
                    errortip = "错误原因：不详\n错误描述：错误原因不明确，请关闭软件重新启动！";
                }
            }
            else
            {
                errorNumCode = "NotFound";
                errortip = "错误原因：不详\n错误描述：错误原因不明确，请关闭软件重新启动！";
            }
            return errortip;
        }
        #region 根路径存储xml的做法
        public static List<string> dealCodeXml(string sty, string _code, string _descript)
        {
            List<string> code_descrip = new List<string>();
            string descrip_value = "", code_value = "";
            sty = sty.ToLower();
            string startPath = Sh_ApplicationStartup.Application_Startup(Application.StartupPath);//因为错误编号和错误解释是放在软件运行路径下的,所以读取的时候需要路径，又是由mldnwl调用的，所以直接用 Application.StartupPath
            XmlDocument xd = new XmlDocument();//创建文档
            xd.Load(startPath+"\\ErrorCode_Description.xml");//Data_errorcode); //加载文档
            if (xd.DocumentElement != null && xd.DocumentElement.Name == "ErrorCode_Description")
            {
                //if (sty.Length == 0)
                //{
                //    code_value = xd.SelectSingleNode("/ErrorCode_Description/_common/code").InnerText.ToString();
                //    descrip_value = xd.SelectSingleNode("/ErrorCode_Description/_common/descript").InnerText.ToString();
                //    code_descrip.Add(code_value);
                //    code_descrip.Add(descrip_value);
                //}
                if (sty.Length>0)//(sty == "_http" || sty == "_sql" || sty == "_merge_add_tods")
                {
                    code_descrip = errorDescript(xd.OuterXml, sty + "s/" + sty, _code);
                    if (code_descrip.Count == 0)
                    {
                        code_descrip = new List<string>();
                        code_descrip = errorDescript(xd.OuterXml, "_keywords/_keyword", _code, _descript);

                    }
                }
                if (code_descrip.Count == 0)
                {
                    code_descrip = new List<string>();
                    code_value = xd.SelectSingleNode("/ErrorCode_Description/_commons/_common/code").InnerText.ToString();
                    descrip_value = xd.SelectSingleNode("/ErrorCode_Description/_commons/_common/descript").InnerText.ToString();
                    code_descrip.Add(code_value);
                    code_descrip.Add(descrip_value);
                }
            }
            else
            {
                code_descrip = new List<string>();
                code_descrip.Add("NotFoundFile");
                code_descrip.Add("没有找到安装路径下的错误代号、错误描述对照文件！");
            }
            return code_descrip;
        }
        //xml带有错误编号的查找解释
       private static List<string> errorDescript(string xml, string sty, string _code)
        {
            List<string> code_descrip = new List<string>();
            string descrip_value = "", code_value = "";
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            XmlNodeList list = xd.SelectNodes("/ErrorCode_Description/" + sty);
            int n = list.Count;
            if (n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    XmlNode DBW_error = list[i];

                    string code = "/ErrorCode_Description/" + sty + "[" + (i + 1).ToString() + "]/code";//code错误代号的路径
                    code_value = xd.SelectSingleNode(code).InnerText.ToString();

                    if (_code == code_value)
                    {
                        string descrip = "/ErrorCode_Description/" + sty + "[" + (i + 1).ToString() + "]/descript";//descript错误描述的路径
                        descrip_value = xd.SelectSingleNode(descrip).InnerText.ToString();
                        code_descrip.Add(code_value);
                        code_descrip.Add(descrip_value);
                    }
                }
            }
            return code_descrip;
        }
       //xml没有带有错误编号的，用关键字匹配查找解释
        private static List<string> errorDescript(string xml, string sty, string _code,string _descript)
        {
            _descript = _descript.ToLower();
            List<string> code_descrip = new List<string>();
            string descrip_value = "", code_value = "";
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            XmlNodeList list = xd.SelectNodes("/ErrorCode_Description/" + sty);
            int n = list.Count;
            if (n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    XmlNode DBW_error = list[i];

                    string code = "/ErrorCode_Description/" + sty + "[" + (i + 1).ToString() + "]/code";//code错误代号的路径
                    code_value = xd.SelectSingleNode(code).InnerText.ToString();

                    if (_code.IndexOf( code_value)>=0 || _descript.IndexOf(code_value)>=0)
                    {
                        string descrip = "/ErrorCode_Description/" + sty + "[" + (i + 1).ToString() + "]/descript";//descript错误描述的路径
                        descrip_value = xd.SelectSingleNode(descrip).InnerText.ToString();
                        code_descrip.Add(code_value+"_"+sty);
                        code_descrip.Add(descrip_value);
                    }
                }
            }
            return code_descrip;
        }
        #endregion

       //调用显示界面
        public static string Handle_WebReturn(string temp, string temp1, int flag)//, out string _errorNumCode//temp 服务器返回的内容//temp1 提示的错误编号或者错误信息//flag=0表示不使用自己的提示语，flag=1表示使用自己的提示语//errorNumCode预防有人需要错误编号的
        {
            // string x2 = temp + temp1;//temp在解析之后本来就是提示语，如： 错误原因：2617\n错误描述：主键冲突；temp1是错误错误编号，如：错误代码：91505192  
            if (flag == 0)
            {
              
                temp1 = dealErrorXml(temp);
              //  _errorNumCode = errorNumCode;

            }
            else
            {
              //  _errorNumCode = "NotFound";
            }
            return temp1;
        }

       


       #endregion

    }
}
