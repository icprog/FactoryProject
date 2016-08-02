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
    /// 设备规格的数据访问类
    /// </summary>
    public  class DA_DeviceSpec
    {
        /// <summary>
        /// 根据ID获取设备规格对象
        /// </summary>
        public static Model.DeviceSpec GetDeviceSpec(Int16 deviceSpecID)
        {
            string sql = "select * from ROSO_DeviceSpec where DeviceSpecID=@DeviceSpecID";
            SqlParameter[] p ={
                new SqlParameter("@DeviceSpecID",deviceSpecID)
            };
            Model.DeviceSpec ds = new Model.DeviceSpec();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                if (dr.Read())
                {
                    ds.ID = Convert.ToInt32(dr["ID"]);
                    ds.DeviceSpecID = Convert.ToInt16(dr["DeviceSpecID"]);
                    ds.DeviceSpecValue = Convert.ToString(dr["DeviceSpecValue"]);
                }
            }
            return ds;
        }

        /// <summary>
        /// 获取所有设备规格信息列表
        /// </summary>
        public static List<Model.DeviceSpec> GetDeviceSpecList()
        {
            string sql = "select * from ROSO_DeviceSpec order by DeviceSpecID";
            List<Model.DeviceSpec> list = new List<Model.DeviceSpec>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(sql, CommandType.Text, null))
            {
                while (dr.Read())
                {
                    Model.DeviceSpec ds = new Model.DeviceSpec();
                    ds.ID = Convert.ToInt32(dr["ID"]);
                    ds.DeviceSpecID = Convert.ToInt16(dr["DeviceSpecID"]);
                    ds.DeviceSpecValue = Convert.ToString(dr["DeviceSpecValue"]);
                    list.Add(ds);
                }
            }
            return list;
        }

        /// <summary>
        /// 添加设备规格信息
        /// </summary>
        public static bool AddDeviceSpec(Model.DeviceSpec ds)
        {
            string sql = "insert into ROSO_DeviceSpec(DeviceSpecID,DeviceSpecValue) values(@DeviceSpecID,@DeviceSpecValue)";
            SqlParameter[] p = {
                new SqlParameter("@DeviceSpecID",ds.DeviceSpecID),
                new SqlParameter("@DeviceSpecValue",ds.DeviceSpecValue)
                    };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }

        /// <summary>
        /// 删除设备规格信息
        /// </summary>
        public static bool DeleteDeviceSpecID(Int16 deviceSpecID)
        {
            string sql = "delete from ROSO_DeviceSpec where DeviceSpecID=@DeviceSpecID";
            SqlParameter[] p = {
                new SqlParameter("@DeviceSpecID",deviceSpecID)
                    };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }

        /// <summary>
        /// 修改设备规格信息
        /// </summary>
        public static bool ChangeDeviceSpec(Model.DeviceSpec ds)
        {
            string sql = "update ROSO_DeviceSpec set DeviceSpecValue=@DeviceSpecValue where DeviceSpecID=@DeviceSpecID";
            SqlParameter[] p = {
                new SqlParameter("@DeviceSpecID",ds.DeviceSpecID),
                new SqlParameter("@DeviceSpec",ds.DeviceSpecValue)
                    };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }


    }
}
