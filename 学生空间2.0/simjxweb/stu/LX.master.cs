using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShDataDLL;
using System.Data;
using System.Data.SqlClient;

public partial class stu_LX : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            string  xyj = Session["userid"].ToString();
            stu_select(xyj);
        }
;    }
    public void stu_select(string temp)
    {
        IDataParameter[] pa = new IDataParameter[2];
        pa[0] = new SqlParameter("@userid", SqlDbType.Int);
        pa[0].Value = temp;// Session["userid"];
        pa[1] = new SqlParameter("@flag", SqlDbType.Int);
        pa[1].Value = 3;
        DataSet ds = DbHelperSQL.ExecProc_Return("stu_select", pa);
        if (ds != null) {
            if (ds.Tables[0] != null){
                username.Text = ds.Tables[0].Rows[0][0].ToString();
                Image1.ImageUrl = ds.Tables[0].Rows[0][1].ToString();
                Imgpath.ImageUrl = ds.Tables[0].Rows[0][1].ToString();

                }
            }
    }
}
