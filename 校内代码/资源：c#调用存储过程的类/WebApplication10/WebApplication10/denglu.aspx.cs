using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;

namespace WebApplication10
{
    public partial class denglu : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = TextBox1.Text;
            Session["stuid"] = name;
            //Response.Redirect("3.aspx");
            Response.Redirect("one_a.aspx");
        }
    }
}