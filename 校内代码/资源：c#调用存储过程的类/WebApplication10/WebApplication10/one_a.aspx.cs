using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShDataDLL;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication10
{
    public partial class one_a : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TextBox2.Text = Session["stuid"].ToString();
            //IDataParameter[] Parameters = new IDataParameter[2];
            //Parameters[0] = new SqlParameter("@stuid ", SqlDbType.Int);
            //Parameters[0].Value = Convert.ToInt32(TextBox2.Text);
            //Parameters[1] = new SqlParameter("@stuname", SqlDbType.VarChar, 8);
            //Parameters[1].Value = string.Empty;
            //Parameters[1].Direction = ParameterDirection.Output;//输出参数
            //DbHelperSQL.ExecProc_NoReturn("stu_select", Parameters);
            ////Parameters[1] = new SqlParameter("@stuname", SqlDbType.VarChar, 8);
            ////DataSet ds=DbHelperSQL.ExecProc_Return("stu_select", Parameters);
            //TextBox1.Text = Parameters[1].Value.ToString();
            //Label k = (Label)(this.Master.FindControl("label1"));
            //k.Text = TextBox1.Text;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IDataParameter[] Parameters = new IDataParameter[2];
            Parameters[0] = new SqlParameter("@stuid ", SqlDbType.Int);
            Parameters[0].Value = Convert.ToInt32(TextBox2.Text);
            Parameters[1] = new SqlParameter("@stuname", SqlDbType.VarChar, 8);
            Parameters[1].Value = string.Empty;
            Parameters[1].Direction = ParameterDirection.Output;//输出参数
            DbHelperSQL.ExecProc_NoReturn("stu_select", Parameters);
            //Parameters[1] = new SqlParameter("@stuname", SqlDbType.VarChar, 8);
            //DataSet ds=DbHelperSQL.ExecProc_Return("stu_select", Parameters);
            TextBox1.Text = Parameters[1].Value.ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label k =(Label)(this.Master.FindControl("label1"));
            k.Text = TextBox1.Text;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            IDataParameter[] pa = new IDataParameter[4];
            pa[0] = new SqlParameter("@stuid", SqlDbType.Int);
            pa[0].Value = 11;
            pa[1] = new SqlParameter("@stulogname", SqlDbType.VarChar, 50);
            pa[1].Value = TextBox4.Text;
            pa[2] = new SqlParameter("@stuschool", SqlDbType.VarChar, 50);
            pa[2].Value = TextBox3.Text;
            pa[3] = new SqlParameter("@stusex", SqlDbType.Char, 1);
            if (TextBox5.Text == "男")
            {
                pa[3].Value = 1;
            }
            else if (TextBox5.Text == "女")
            {
                pa[3].Value = 2;
            }
            DbHelperSQL.ExecProc_NoReturn("set_update", pa);
        }
    }
}