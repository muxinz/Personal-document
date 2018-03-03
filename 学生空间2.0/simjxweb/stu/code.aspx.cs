using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class stu_code : System.Web.UI.Page

{
  //  public string w;
    private string w;
    public string luo
        {
        get { return w; }
        //set {}
        }


    public string GenCode(int num)
    {
        string[] source ={"0","1","2","3","4","5","6","7","8","9",
                         "A","B","C","D","E","F","G","H","I","J","K","L","M","N",
                       "O","P","Q","R","S","T","U","V","W","X","Y","Z"};
        string code = "";
        Random rd = new Random();//随机函数
        for (int i = 0; i < num; i++)
        {
            code += source[rd.Next(0, source.Length)];
        }
        w = code;
        
        return code;
    }
    public void GenImg(string code)
    {
        Bitmap myPalette = new Bitmap(45, 16);//定义一个画板
        Graphics gh = Graphics.FromImage(myPalette);//在画板上定义绘图的实例
        Rectangle rc = new Rectangle(0, 0, 45, 16);//定义一个矩形
        gh.FillRectangle(new SolidBrush(Color.CornflowerBlue), rc);//填充矩形
        gh.DrawString(code, new Font("宋体", 13), new SolidBrush(Color.White), rc);//在矩形内画出字符串
        //myPalette.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);//将图片显示出来
        //pictureBox1.Image = Image.FromHbitmap(myPalette.GetHbitmap());
        //ImageMap1 = Image.FromHbitmap(myPalette.GetHbitmap());
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        myPalette.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        Response.BinaryWrite(ms.GetBuffer());
        gh.Dispose();
        myPalette.Dispose();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string yu = GenCode(4);
        Session["code"] = yu;
        GenImg(yu);
        
        //Response.Redirect("login.aspx?name=w");
    }
}