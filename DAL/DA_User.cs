/*
                   _ooOoo_
                  o8888888o
                  88" . "88
                  (| -_- |)
                  O\  =  /O
               ____/`---'\____
             .'  \\|     |//  `.
            /  \\|||  :  |||//  \
           /  _||||| -:- |||||-  \
           |   | \\\  -  /// |   |
           | \_|  ''\---/''  |   |
           \  .-\__  `-`  ___/-. /
         ___`. .'  /--.--\  `. . __
      ."" '<  `.___\_<|>_/___.'  >'"".
     | | :  `- \`.;`\ _ /`;.`/ - ` : | |
     \  \ `-.   \_ __\ /__ _/   .-` /  /
======`-.____`-.___\_____/___.-`____.-'======
                   `=---='
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
         佛祖保佑       永无BUG
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ROSO.DAL
{
    /// <summary>
    /// 用户数据操作类
    /// </summary>
    public class DA_User
    {
        /// <summary>
        /// 根据帐号获取用户
        /// </summary>
        public static Model.ROSOUser GetUser(string account)
        {
            string sql = "select * from ROSO_User where Account=@Account";
            SqlParameter[] p ={
                new SqlParameter("@Account",account)
            };
            Model.ROSOUser user = new Model.ROSOUser();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                if (dr.Read())
                {
                    user.ID = Convert.ToInt32(dr["ID"]);
                    user.Account = Convert.ToString(dr["Account"]);
                    user.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"]);
                    user.Password = Convert.ToString(dr["Password"]);
                    user.Role = Convert.ToString(dr["Role"]);
                }
            }
            return user;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        public static bool Login(string account, string password)
        {
            string sql = "select count(*) from ROSO_User where Account=@Account and Password=@Password";
            SqlParameter[] p ={
                new SqlParameter("@Account",account),
                new SqlParameter("@Password",password)
            };
            int i = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, CommandType.Text, p));
            return i > 0;
        }

        /// <summary>
        /// 获取指定角色用户列表
        /// </summary>
        public static List<Model.ROSOUser> GetUserListByRole(string role)
        {
            string sql = "select * from ROSO_User where Role=@Role";
            SqlParameter[] p ={
                new SqlParameter("@Role",role)
            };
            List<Model.ROSOUser> list = new List<Model.ROSOUser>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                while (dr.Read())
                {
                    Model.ROSOUser user = new Model.ROSOUser();
                    user.ID = Convert.ToInt32(dr["ID"]);
                    user.Account = Convert.ToString(dr["Account"]);
                    user.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"]);
                    user.Password = Convert.ToString(dr["Password"]);
                    user.Role = Convert.ToString(dr["Role"]);
                    list.Add(user);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        public static List<Model.ROSOUser> GetUserList()
        {
            string sql = "select * from ROSO_User order by Role desc";
            List<Model.ROSOUser> list = new List<Model.ROSOUser>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(sql, CommandType.Text, null))
            {
                while (dr.Read())
                {
                    Model.ROSOUser user = new Model.ROSOUser();
                    user.ID = Convert.ToInt32(dr["ID"]);
                    user.Account = Convert.ToString(dr["Account"]);
                    user.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"]);
                    user.Password = Convert.ToString(dr["Password"]);
                    user.Role = Convert.ToString(dr["Role"]);
                    list.Add(user);
                }
            }
            return list;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        public static bool AddUser(Model.ROSOUser user)
        {
            string sql = "insert into ROSO_User(Account,Password,Role,LastLoginTime) values(@Account,@Password,@Role,@LastLoginTime)";
            SqlParameter[] p = {
                new SqlParameter("@Account",user.Account),
                new SqlParameter("@Password",user.Password),
                new SqlParameter("@Role",user.Role),
                new SqlParameter("@LastLoginTime",user.LastLoginTime)
                    };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public static bool DeleteUser(string account)
        {
            string sql = "delete from ROSO_User where Account=@Account";
            SqlParameter[] p = {
                new SqlParameter("@Account",account)
                    };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        public static bool ChangePassword(string account, string password)
        {
            string sql = "update ROSO_User set Password=@Password where Account=@Account";
            SqlParameter[] p = {
                new SqlParameter("@Password",password),
                new SqlParameter("@Account",account)
                    };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }

        /// <summary>
        /// 修改登录时间
        /// </summary>
        public static bool ChangeLastLoginTime(string account)
        {
            string sql = "update ROSO_User set LastLoginTime=@LastLoginTime where Account=@Account";
            SqlParameter[] p = {
                        new SqlParameter("@LastLoginTime", DateTime.Now),
                        new SqlParameter("@Account",account)
                    };
            return (SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p) > 0);
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        public static bool UserIsExist(string account)
        {
            string sql = "select count(*) from ROSO_User where Account=@Account";
            SqlParameter[] p ={
                new SqlParameter("@Account",account)
            };
            int i = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, CommandType.Text, p));
            return i > 0;
        }
    }
}
