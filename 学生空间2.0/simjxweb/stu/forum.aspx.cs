using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShDataDLL;
using System.Data;
using System.Data.SqlClient;

public partial class stu_forum : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        YEAR.Text = DateTime.Now.Year.ToString();
        DAY.Text = DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日";
        change_master();
        IDataParameter[] pa = new IDataParameter[1];
        pa[0] = new SqlParameter("@stuid", SqlDbType.Int);
        pa[0].Value = Session["userid"];
        DataSet ds = DbHelperSQL.ExecProc_Return("stuforum_select", pa);
        DataList1.DataSource = ds.Tables[0];
        DataList1.DataBind();
        //int i = 0;
        //i = ds.Tables[0].Rows.Count;
        //string[] q = new string[i];
        //for (int k=0;k<ds.Tables[0].Rows.Count;k++)
        //{
        //    q[k] = ds.Tables[0].Rows[k]["detail"].ToString();
        //    Session["detail"] = q[k];
        //    Response.Write(q[k].ToString()+"<br>");
        //}
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