using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShDataDLL;
using System.Data;
using System.Data.SqlClient;
public partial class stu_set_a : System.Web.UI.Page
{
    //string Phone;
    //string Email;
    //string Password;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        YEAR.Text = DateTime.Now.Year.ToString();
        DAY.Text = DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日";
        if (Session["userid"] != null)
        {
            string temp = Session["userid"].ToString();
            set(temp);
        }
    }
    public void set(string temp)
    {
        IDataParameter[] parameters = new IDataParameter[2];
        parameters[0] = new SqlParameter("@userid", SqlDbType.Int);
        parameters[0].Value = temp;
        parameters[1] = new SqlParameter("@flag", SqlDbType.Int);
        parameters[1].Value = "2";
        DataSet ds = DbHelperSQL.ExecProc_Return("stu_select", parameters);
        if (ds != null && ds.Tables[0] != null)
        {
            //DataList1.DataSource = ds.Tables[0];
            //DataList1.DataBind();
            if (!IsPostBack)
            {
                phone.Text = ds.Tables[0].Rows[0][0].ToString();
                email.Text = ds.Tables[0].Rows[0][1].ToString();
                oldword.Text = ds.Tables[0].Rows[0][2].ToString();
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Session["userid"] != null)
        {
            string lz = Session["userid"].ToString();
            IDataParameter[] pa = new IDataParameter[4];
            pa[0] = new SqlParameter("@userid", SqlDbType.Int);
            pa[0].Value = lz;
            pa[1] = new SqlParameter("@Phone", SqlDbType.VarChar, 50);
            pa[1].Value = phone.Text;
            pa[2] = new SqlParameter("@email", SqlDbType.VarChar, 50);
            pa[2].Value = email.Text;
            pa[3] = new SqlParameter("@password", SqlDbType.VarChar, 50);
            if (newword.Text != "")
            {
                if (newword.Text != quereng.Text)
                {
                    Label1.Text = "密码输入不一致";
                }
                else
                {
                    pa[3].Value = newword.Text;
                    DbHelperSQL.ExecProc_Return("stu_aupdate", pa);
                    Response.Redirect("set.aspx");
                }
            }
            else
            {
                Label1.Text = "请输入新密码";
            }
        }
    }
}