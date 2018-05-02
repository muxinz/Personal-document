using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShDataDLL;
namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //  DbHelperSQL xyj = new DbHelperSQL();
            // DbHelperSQL.
            DBClass kk = new DBClass();
            kk.ExecuteSql("insert into Admin values(1,'adminname','adminname','3033927590',00);");
        }
    }
}