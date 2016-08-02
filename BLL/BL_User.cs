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
using System.Web.Security;

namespace ROSO.BLL
{
    /// <summary>
    /// 用户的业务逻辑类
    /// </summary>
    public class BL_User
    {
        /// <summary>
        /// 根据帐号获取用户
        /// </summary>
        public static Model.ROSOUser GetUser(string account)
        {
            return DAL.DA_User.GetUser(account);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        public static bool Login(string account, string password)
        {
            password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            return DAL.DA_User.Login(account, password);
        }

        /// <summary>
        /// 获取指定角色用户列表
        /// </summary>
        public static List<Model.ROSOUser> GetUserListByRole(string role)
        {
            return DAL.DA_User.GetUserListByRole(role);
        }

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        public static List<Model.ROSOUser> GetUserList()
        {
            return DAL.DA_User.GetUserList();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        public static bool AddUser(Model.ROSOUser user)
        {
            user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5");
            return DAL.DA_User.AddUser(user);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public static bool DeleteUser(string account)
        {
            return DAL.DA_User.DeleteUser(account);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        public static bool ChangePassword(string account, string oldPassword,string newPassword)
        {
            string dbPassword = GetUser(account).Password;
            oldPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(oldPassword, "MD5");
            if (string.Equals(dbPassword, oldPassword))
            {
                return DAL.DA_User.ChangePassword(account, newPassword);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改登录时间
        /// </summary>
        public static bool ChangeLastLoginTime(string account)
        {
            return DAL.DA_User.ChangeLastLoginTime(account);
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        public static bool UserIsExist(string account)
        {
            return DAL.DA_User.UserIsExist(account);
        }
    }
}
