using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShDataDLL;
using System.Data;
using System.Data.SqlClient;

public partial class stu_spread : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            IDataParameter[] pa = new IDataParameter[2];
            pa[0] = new SqlParameter("@userid", SqlDbType.Int);
            pa[0].Value = Session["userid"];
            pa[1] = new SqlParameter("@flag", SqlDbType.Int);
            pa[1].Value = 5;
            DataSet ds = DbHelperSQL.ExecProc_Return("stu_select", pa);
            if (ds != null && ds.Tables[0] != null)
            {
                Label1.Text = ds.Tables[0].Rows[0][0].ToString();
            }
      }

    }

}