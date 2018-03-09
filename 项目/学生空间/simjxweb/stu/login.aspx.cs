using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class stu_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["userid"] = null;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string kk = Session["code"].ToString();
        if (TextBox3.Text != kk)
        {
            Image1.ImageUrl = "code.aspx";
        }
        else
        {
        string name = TextBox1.Text;
        Session["userid"] = name;
        Response.Redirect("homepage_new.aspx");
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
       
        Image1.ImageUrl = "code.aspx";
        //string mm = Request.QueryString["name"].ToString();
        //TextBox2.Text = mm;
        
        
    }
}