using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShDataDLL;

namespace ShDataDLL
{
    public class GetComInfo
    {
        #region 获得一个公司的全部角色
        public static DataTable getcomChara(int comid)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@comid", SqlDbType.Int);
            para[0].Value = comid;
            string sql = "select stafflevel,charactername from CharacterInfo where comid=@comid";
            DataTable dt = new DataTable();
            dt = DbHelperSQL.QueryTB(sql, para);
            return dt;
        }
        #endregion

        #region 获得一个公司的所有货站
        public static DataTable getcomWh(int comid)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@comid", SqlDbType.Int);
            para[0].Value = comid;
            string sql = "OPEN SYMMETRIC KEY [ShKey]    DECRYPTION BY CERTIFICATE MyServerCert ";
            sql += "select dbo.shdes2(ShWname) as ShWname,whid from WareHouse where comid=@comid";
            sql += " CLOSE SYMMETRIC KEY [ShKey]";
            DataTable dt = new DataTable();
            dt = DbHelperSQL.QueryTB(sql, para);
            return dt;
        }
        #endregion

        #region 获得一个货站的所有网点
        public static DataTable getcomBranch(string whid)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@whid", SqlDbType.VarChar, 6);
            para[0].Value = whid;
            string sql = "OPEN SYMMETRIC KEY [ShKey]    DECRYPTION BY CERTIFICATE MyServerCert ";
            sql += "select dbo.shdes2(branchname) as branchname,branchid from BranchInfo where whid =@whid";
            sql += " CLOSE SYMMETRIC KEY [ShKey]";
            DataTable dt = new DataTable();
            dt = DbHelperSQL.QueryTB(sql, para);
            return dt;
        }
        #endregion

        #region 获取货站中一个员工的所有详情
        public static DataTable getStaffInfo(string staffid)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@staffid", SqlDbType.VarChar, 8);
            para[0].Value = staffid;
            string sql = "OPEN SYMMETRIC KEY [ShKey]    DECRYPTION BY CERTIFICATE MyServerCert ";
            sql += "select staffid,staffname,sex,idnumber,StaffInfo.address,mobile,staffremark,stafflevel,whid,branchid,comid,dbo.shdes2(ShMypassword) as ShMypassword ";
            sql += "from StaffInfo where staffid=@staffid ";
            sql += "CLOSE SYMMETRIC KEY [ShKey]";
            DataTable dt = new DataTable();
            dt = DbHelperSQL.QueryTB(sql, para);
            return dt;
        }
        #endregion 
    }
}
