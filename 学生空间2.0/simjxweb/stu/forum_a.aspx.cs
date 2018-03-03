using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShDataDLL;
using System.Data;
using System.Data.SqlClient;

public partial class stu_forum_a : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        change_master();
        YEAR.Text = DateTime.Now.Year.ToString();
        DAY.Text = DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日";
    }
    public void change_master()
    {
        IDataParameter[] parameters = new IDataParameter[2];
        parameters[0] = new SqlParameter("@stuid", SqlDbType.Int);
        parameters[0].Value = Session["userid"];
        parameters[1] = new SqlParameter("@stuname", SqlDbType.VarChar, 8);
        parameters[1].Direction = ParameterDirection.Output;
        DbHelperSQL.ExecProc_NoReturn("stu_select", parameters);
        Label k = (Label)(this.Master.FindControl("stuname"));
        k.Text = parameters[1].Value.ToString();
    }
}