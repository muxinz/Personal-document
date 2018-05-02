using System;
using ShDataDLL;
using System.Data;
using System.Data.SqlClient;
namespace WebApplication10
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //删除
            //DBClass db = new DBClass();
            //db.ExecuteSql("insert into Customer(Username,Realname,Phonecode,Email,Buynumber)values('5599','88','123456','444',44);");
            IDataParameter[] Parameters = new IDataParameter[1];
            Parameters[0] = new SqlParameter("@Username ",SqlDbType.VarChar,50);
            Parameters[0].Value = 55;
            DbHelperSQL.ExecProc_NoReturn("sim_delete", Parameters);


        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //修改
            IDataParameter[] Parameters = new IDataParameter[7];
            Parameters[0] = new SqlParameter("@CustomerID ",SqlDbType.Int);
            Parameters[0].Value = 10;
            Parameters[1] = new SqlParameter("@Username",SqlDbType.VarChar,50);
            Parameters[1].Value = "黄阳";
            Parameters[2] = new SqlParameter("@Password",SqlDbType.VarChar,50);
            Parameters[2].Value = "hy";
            Parameters[3] = new SqlParameter("@Realname",SqlDbType.VarChar,50);
            Parameters[3].Value = "黄阳阳";
            Parameters[4] = new SqlParameter("@Phonecode",SqlDbType.VarChar,20);
            Parameters[4].Value = "1587144";
            Parameters[5] = new SqlParameter("@Email",SqlDbType.VarChar,50);
            Parameters[5].Value = "30339275";
            Parameters[6] = new SqlParameter("@Buynumber",SqlDbType.Int);
            Parameters[6].Value = 44;
            DbHelperSQL.ExecProc_NoReturn("sim_update", Parameters);

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //插入
            IDataParameter[] Parameters = new IDataParameter[6];
       
            Parameters[0] = new SqlParameter("@Username", SqlDbType.VarChar, 50);
            Parameters[0].Value = "黄阳";
            Parameters[1] = new SqlParameter("@Password", SqlDbType.VarChar, 50);
            Parameters[1].Value = "hy";
            Parameters[2] = new SqlParameter("@Realname", SqlDbType.VarChar, 50);
            Parameters[2].Value = "黄阳阳";
            Parameters[3] = new SqlParameter("@Phonecode", SqlDbType.VarChar, 20);
            Parameters[3].Value = "1587144";
            Parameters[4] = new SqlParameter("@Email", SqlDbType.VarChar, 50);
            Parameters[4].Value = "30339275";
            Parameters[5] = new SqlParameter("@Buynumber", SqlDbType.Int);
            Parameters[5].Value = 44;
            DbHelperSQL.ExecProc_NoReturn("sim_iensrt", Parameters);

        }
    }
}