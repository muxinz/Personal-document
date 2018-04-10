using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace creat_xml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public XmlDocument xmlDoc;
        public XmlElement xmlEle;
        public XmlNode xmlNod;
        //创建
        public void create_xml()
        {
            xmlDoc = new XmlDocument();
            XmlDeclaration xmldec;//声明段落，xml开始的头
            xmldec = xmlDoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmlDoc.AppendChild(xmldec);//appendChild() 方法可向节点的子节点列表的末尾添加新的子节点。此方法可返回这个新的子节点。
            xmlEle = xmlDoc.CreateElement("", "Employees", "");//添加新的节点（根元素）
            xmlDoc.AppendChild(xmlEle);
            XmlNode root = xmlDoc.SelectSingleNode("Employees");//查找<Employees>作为根节点
            for(int i=1;i<3;i++)
            {
            XmlElement lz = xmlDoc.CreateElement("Node");//创建一个Node的节点
            lz.SetAttribute("g", "where");//设置g属性
            XmlElement lz1 = xmlDoc.CreateElement("tittle");//设置新的tittle节点
            lz1.InnerText = "这里是tittle第一行";
            lz.AppendChild(lz1);//将tittle加到Node中
            XmlElement lz2 = xmlDoc.CreateElement("author");//设置新的tittle节点
            lz2.InnerText = "这里是tittle第2行";
            lz.AppendChild(lz2);
            root.AppendChild(lz);//<Employees>把整体包进来
            }
            xmlDoc.Save("1.xml");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            create_xml();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string k;
            XmlDocument xml = new XmlDocument();
            //if(System.IO.File.Exists("1.xml"))
            //{
            //    xml.Load("1.xml");
            //}
            xml.Load("1.xml");
            k = xml.InnerText;//读取xml中所有文本的内容
            richTextBox1.Text = k;
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string k;
            XmlDocument xd = new XmlDocument();
            if (System.IO.File.Exists("SystemParameterAdd.xml"))
            {
                xd.Load("SystemParameterAdd.xml");//加载文档
                XmlNodeList xxlist = xd.GetElementsByTagName("SystemParameter");//所有节点
                foreach (XmlNode xxNode in xxlist)//单个节点
                {
                    XmlNodeList xxlist1 = xxNode.ChildNodes;//获取子节点
                    foreach (XmlNode xxNode1 in xxlist1)
                    {
                        XmlNodeList xxlist2 = xxNode1.ChildNodes;//获取子节点
                        foreach (XmlNode xxNode2 in xxlist2)
                        {
                            Console.WriteLine("6");
                            if (xxNode2.Name == "MainIniVersion") /*LocationInformation*/
                            {
                                k = xxNode2.InnerText.Trim();
                                richTextBox1.Text = k;
                            Console.WriteLine("7");
                        }
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            XmlDocument xml = new XmlDocument();
            if (System.IO.File.Exists("1.xml"))
            {
                xml.Load("1.xml");
                XmlNodeList list = xml.GetElementsByTagName("Employees");
                foreach(XmlNode lz in list)
                {
                    XmlNodeList list1 = lz.ChildNodes;
                    foreach (XmlNode lz1 in list1)
                    {
                        XmlNodeList list2 = lz1.ChildNodes;
                        foreach(XmlNode lz2 in list2)
                        { 
                             Console.WriteLine("11");
                            if(lz2.Name== "tittle")
                        {
                                lz2.InnerText = "9999";
                                lz1.RemoveChild(lz2);
                             richTextBox1.Text = xml.InnerText;
                                xml.Save("2.xml");
                             Console.WriteLine("22");
                    }
                    }
                    }
                }
            }

           }
        //修改
        private void button5_Click(object sender, EventArgs e)
        {
            string k;
            XmlDocument xml = new XmlDocument();
            if (System.IO.File.Exists("SystemParameterAdd.xml"))
            {
                xml.Load("SystemParameterAdd.xml");
                XmlNodeList list = xml.GetElementsByTagName("SystemParameter");
                foreach (XmlNode lz in list)
                {
                    XmlNodeList list1 = lz.ChildNodes;
                    foreach (XmlNode lz1 in list1)
                    {
                        XmlNodeList list2 = lz1.ChildNodes;
                        foreach (XmlNode lz2 in list2)
                        {
                            Console.WriteLine("ok?");
                            if (lz2.Name == "MainIniVersion")
                            {

                                lz2.InnerText = "0000" + "修改的这个地方";
                                k = lz2.InnerText ;
                                richTextBox1.Text = k;
                            }
                        }
                    }
                }
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("1.xml");
            XmlNode lz = xml.SelectSingleNode("Employees");//查找xml文件中Node的节点
            XmlElement xmle = xml.CreateElement("Node");
            XmlElement xmle1 = xml.CreateElement("new_tittle");
            xmle1.InnerText = "这是新加的节点";
            xmle.AppendChild(xmle1);
            lz.AppendChild(xmle);
            Console.WriteLine("1");
            richTextBox1.Text = xml.InnerText;
        }
    }
}
