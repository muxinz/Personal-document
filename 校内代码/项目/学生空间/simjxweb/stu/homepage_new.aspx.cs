using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShDataDLL;
using System.Data;
using System.Data.SqlClient;

public partial class stu_homepage_new : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            string temp = Session["userid"].ToString();
            set(temp);
        }
    }
    public void set(string temp)
    {

        IDataParameter[] pa = new IDataParameter[2];
        pa[0] = new SqlParameter("@userid", SqlDbType.Int);
        pa[0].Value = temp;
        pa[1] = new SqlParameter("@flag", SqlDbType.Int);
        pa[1].Value = 4;
        DataSet ds = DbHelperSQL.ExecProc_Return("stu_select", pa);
        //DataList1.DataSource = ds.Tables[0];
        //DataList1.DataBind();
        if (ds != null && ds.Tables[0] != null)
        { 
        stulogname.Text = ds.Tables[0].Rows[0][0].ToString();
        //stuschool.Text = ds.Tables[0].Rows[0][1].ToString();
        //phone.Text = ds.Tables[0].Rows[0][3].ToString();
       // email.Text = ds.Tables [0].Rows[0][4].ToString();
        //string q3 = ds.Tables[0].Rows[0][2].ToString();
        // if (q3 == "1")
        //    {
        //        stusex.Text = "男";
        //    }
        // else if (q3 == "2")
        //    {
        //        stusex.Text = "女";
        //    }
        }
    }
    
}