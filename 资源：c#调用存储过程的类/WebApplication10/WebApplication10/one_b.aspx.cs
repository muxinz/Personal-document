using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using ShDataDLL;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication10
{
    public partial class one_b : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IDataParameter[] Parameters = new IDataParameter[2];
            Parameters[0] = new SqlParameter("@stuid ", SqlDbType.Int);
            Parameters[0].Value = Session["stuid"];
            Parameters[1] = new SqlParameter("@stuname", SqlDbType.VarChar, 8);
            Parameters[1].Value = string.Empty;
            Parameters[1].Direction = ParameterDirection.Output;//输出参数
            DbHelperSQL.ExecProc_NoReturn("stu_select", Parameters);
            //Parameters[1] = new SqlParameter("@stuname", SqlDbType.VarChar, 8);
            //DataSet ds=DbHelperSQL.ExecProc_Return("stu_select", Parameters);
            Label k = (Label)(this.Master.FindControl("label1"));
            k.Text = Parameters[1].Value.ToString();
        }
    }
}