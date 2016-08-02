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
    /// 设备信息模板的数据访问类
    /// </summary>
    public class DA_DeviceTemplate
    {
        /// <summary>
        /// 获取指定模板的指定项目信息
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public static Model.DeviceTemplate GetDeviceTemplate(Model.DeviceType deviceType,string project)
        {
            string sql = "";
            switch (deviceType)
            {
                case Model.DeviceType.ROSO_RSJ:
                    sql = "select * from ROSO_RSJ where Project=@Project";
                    break;
                case Model.DeviceType.ROSO_BZJ:
                    sql = "select * from ROSO_BZJ where Project=@Project";
                    break;
                case Model.DeviceType.ROSO_DXJ:
                    sql = "select * from ROSO_DXJ where Project=@Project";
                    break;
                case Model.DeviceType.ROSO_RDJ:
                    sql = "select * from ROSO_RDJ where Project=@Project";
                    break;
                default:
                    return null;
                    break;
            }
            SqlParameter[] p ={
                new SqlParameter("@Project",project)
            };
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                Model.DeviceTemplate dt = new Model.DeviceTemplate();
                if (reader.Read())
                {
                    dt.ID = Convert.ToInt32(reader["ID"]);
                    dt.Address = Convert.ToInt16(reader["Address"]);
                    dt.Length = Convert.ToInt16(reader["Length"]);
                    dt.Project = Convert.ToString(reader["Project"]);
                    dt.Remark = Convert.ToString(reader["Remark"]);
                    dt.Type = Convert.ToString(reader["Type"]);
                }
                return dt;
            }
        }

        /// <summary>
        /// 获取指定模板的全部信息
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public static List<Model.DeviceTemplate> GetDeviceTemplateList(Model.DeviceType deviceType)
        {
            string sql;
            switch (deviceType)
            {
                case Model.DeviceType.ROSO_RSJ:
                    sql = "select * from ROSO_RSJ";
                    break;
                case Model.DeviceType.ROSO_BZJ:
                    sql = "select * from ROSO_BZJ";
                    break;
                case Model.DeviceType.ROSO_DXJ:
                    sql = "select * from ROSO_DXJ";
                    break;
                case Model.DeviceType.ROSO_RDJ:
                    sql = "select * from ROSO_RDJ";
                    break;
                default:
                    return null;
                    break;
            }
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, null))
            {
                List<Model.DeviceTemplate> list = new List<Model.DeviceTemplate>();
                while (reader.Read())
                {
                    Model.DeviceTemplate dt = new Model.DeviceTemplate();
                    dt.ID = Convert.ToInt32(reader["ID"]);
                    dt.Address = Convert.ToInt16(reader["Address"]);
                    dt.Length = Convert.ToInt16(reader["Length"]);
                    dt.Project = Convert.ToString(reader["Project"]);
                    dt.Remark = Convert.ToString(reader["Remark"]);
                    dt.Type = Convert.ToString(reader["Type"]);
                    list.Add(dt);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取指定类型设备模板的项目列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDeviceTemplateProjectList(Model.DeviceType deviceType)
        {
            string sql;
            switch (deviceType)
            {
                case Model.DeviceType.ROSO_RSJ:
                    sql = "select Project from ROSO_RSJ";
                    break;
                case Model.DeviceType.ROSO_BZJ:
                    sql = "select Project from ROSO_BZJ";
                    break;
                case Model.DeviceType.ROSO_DXJ:
                    sql = "select Project from ROSO_DXJ";
                    break;
                case Model.DeviceType.ROSO_RDJ:
                    sql = "select Project from ROSO_RDJ";
                    break;
                default:
                    return null;
                    break;
            } 
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, null))
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(Convert.ToString(reader["Project"]));
                }
                return list;
            }
        }

        /// <summary>
        /// 获取指定类型模板的字节总数
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public static int GetDeviceTemplateByteCount(Model.DeviceType deviceType)
        {
            string sql;
            switch (deviceType)
            {
                case Model.DeviceType.ROSO_RSJ:
                    sql = "select Sum(Length) from ROSO_RSJ";
                    break;
                case Model.DeviceType.ROSO_BZJ:
                    sql = "select Sum(Length) from ROSO_BZJ";
                    break;
                case Model.DeviceType.ROSO_DXJ:
                    sql = "select Sum(Length) from ROSO_DXJ";
                    break;
                case Model.DeviceType.ROSO_RDJ:
                    sql = "select Sum(Length) from ROSO_RDJ";
                    break;
                default:
                    return 0;
                    break;
            }
            int i = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, CommandType.Text, null));
            return i;
        }


    }
}
