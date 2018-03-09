using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShDataDLL;
using System.Data;
using System.Data.SqlClient;

public partial class stu_index : System.Web.UI.Page
{
    public string temp = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userid"] != null)
        {
             temp = Session["userid"].ToString();
            
        }
        db_bind(temp);
        //日期
        YEAR.Text = DateTime.Now.Year.ToString();
        DAY.Text = DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日";
    }
    public void db_bind(string temp)
    {
        IDataParameter[] pa = new IDataParameter[1];
        pa[0] = new SqlParameter("@userid", SqlDbType.Int);
        pa[0].Value = temp;
        DataSet ds = DbHelperSQL.ExecProc_Return("stucourse_select", pa);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        PagedDataSource db = new PagedDataSource();
        db.DataSource = dt.DefaultView;//表数据赋给页
        db.AllowPaging = true;//显示分页
        db.PageSize = 4;
        int currentIndex = Convert.ToInt32(this.current.Text) - 1;
        this.first.Enabled = true;
        this.page_up.Enabled = true;
        this.page_down.Enabled = true;
        if (currentIndex == 0)//如果是第一页
        {
            this.first.Enabled = false;//首页不可用
            this.page_up.Enabled = false;//上一页不可用
            this.page_down.Enabled = true;//下一页可用
        }
        if (currentIndex != 0&&currentIndex == db.PageCount - 1)//如果最后一页
        {
            this.first.Enabled = true;//首页可用
            this.page_up.Enabled = true;//上一页可用
            this.page_down.Enabled = false;//下一页不可用
        }
        if (db.PageCount == 1)
        {
            this.first.Enabled = false;//首页不可用
            this.page_up.Enabled = false;//上一页不可用
            this.page_down.Enabled = false;//下一页不可用
        }
        db.CurrentPageIndex = currentIndex;
        pagecount.Text = "共" + db.PageCount + "页";
        this.DataList1.DataSource = db;
        DataList1.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        current.Text = "1";
        db_bind(temp);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        int i = Convert.ToInt32(current.Text);
        current.Text = Convert.ToString(i - 1);
        db_bind(temp);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = Convert.ToInt32(current.Text);
        current.Text = Convert.ToString(i + 1);
        db_bind(temp);

    }
}