using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShDataDLL;
using System.Data;
using System.Data.SqlClient;


public partial class stu_set : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        YEAR.Text = DateTime.Now.Year.ToString();
        DAY.Text = DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日";
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                set_edit();
            }
        }
        //ScriptManager1.RegisterAsyncPostBackControl(this.Button2);

    }
    public void set_edit()
    {
        if (Request["stulogname"].ToString() != null)
        {
            string q1 = Request["stulogname"].ToString();
            TextBox1.Text = q1;
        }
        if (Request["stuschool"].ToString() != null)
        {
            string q2 = Request["stuschool"].ToString();
            TextBox4.Text = q2;
        }
        if (Request["sex"].ToString() != null)
        {
            string q3 = Request["sex"].ToString();
            TextBox2.Text = q3;
        }
        if (Request["stugrade"].ToString() != null)
        {
            string q4 = Request["stugrade"].ToString();
            TextBox3.Text = q4;
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            string lz = Session["userid"].ToString();
            IDataParameter[] pa = new IDataParameter[5];
            pa[0] = new SqlParameter("@userid", SqlDbType.Int);
            pa[0].Value = lz;
            pa[1] = new SqlParameter("@stulogname", SqlDbType.VarChar, 50);
            pa[1].Value = TextBox1.Text;
            pa[2] = new SqlParameter("@stuschool", SqlDbType.VarChar, 50);
            pa[2].Value = TextBox4.Text;
            pa[3] = new SqlParameter("@stusex", SqlDbType.Char, 1);
            if (TextBox2.Text == "男")
            {
                pa[3].Value = 1;
            }
            else if (TextBox2.Text == "女")
            {
                pa[3].Value = 2;
            }
            pa[4] = new SqlParameter("@stugrade", SqlDbType.VarChar, 6);
            pa[4].Value = TextBox3.Text;
            DbHelperSQL.ExecProc_Return("stu_update", pa);
            //UpdatePanel1.Update();
            Response.Redirect("set.aspx");
        }
    }

}