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
    /// 设备基本信息的数据访问类
    /// </summary>
    public class DA_DeviceInfo
    {
        /// <summary>
        /// 按编号获取设备基本信息
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public static Model.DeviceInfo GetDeviceInfoByID(Int16 deviceID)
        {
            string sql = "select * from ROSO_DeviceInfo where DeviceID=@DeviceID";
            SqlParameter[] p ={
                new SqlParameter("@DeviceID",deviceID)
            };
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                Model.DeviceInfo di = new Model.DeviceInfo();
                if (reader.Read())
                {
                    di.ID = Convert.ToInt32(reader["ID"]);
                    di.DeviceID = Convert.ToInt16(reader["DeviceID"]);
                    di.DeviceIP = Convert.ToString(reader["DeviceIP"]);
                    di.DeviceName = Convert.ToString(reader["DeviceName"]);
                    di.DeviceStatus = Convert.ToInt16(reader["DeviceStatus"]);
                    di.DeviceType = Convert.ToString(reader["DeviceType"]);
                    di.Remark = Convert.ToString(reader["Remark"]);
                    di.DeviceSpec = Convert.ToString(reader["DeviceSpec"]);
                }
                return di;
            }
        }

        /// <summary>
        /// 按IP地址获取设备基本信息
        /// </summary>
        /// <param name="deviceIP"></param>
        /// <returns></returns>
        public static Model.DeviceInfo GetDeviceInfoByIP(string deviceIP)
        {
            string sql = "select * from ROSO_DeviceInfo where DeviceIP=@DeviceIP";
            SqlParameter[] p ={
                new SqlParameter("@DeviceIP",deviceIP)
            };
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                Model.DeviceInfo di = new Model.DeviceInfo();
                if (reader.Read())
                {
                    di.ID = Convert.ToInt32(reader["ID"]);
                    di.DeviceID = Convert.ToInt16(reader["DeviceID"]);
                    di.DeviceIP = Convert.ToString(reader["DeviceIP"]);
                    di.DeviceName = Convert.ToString(reader["DeviceName"]);
                    di.DeviceStatus = Convert.ToInt16(reader["DeviceStatus"]);
                    di.DeviceType = Convert.ToString(reader["DeviceType"]);
                    di.Remark = Convert.ToString(reader["Remark"]);
                    di.DeviceSpec = Convert.ToString(reader["DeviceSpec"]);
                }
                return di;
            }
        }

        /// <summary>
        /// 获取全部设备的基本信息列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.DeviceInfo> GetDeviceInfoList()
        { 
            string sql = "select * from ROSO_DeviceInfo order by DeviceType,DeviceIP";
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, null))
            {
                List<Model.DeviceInfo> list = new List<Model.DeviceInfo>();
                while (reader.Read())
                {
                    Model.DeviceInfo di = new Model.DeviceInfo();
                    di.ID = Convert.ToInt32(reader["ID"]);
                    di.DeviceID = Convert.ToInt16(reader["DeviceID"]);
                    di.DeviceIP = Convert.ToString(reader["DeviceIP"]);
                    di.DeviceName = Convert.ToString(reader["DeviceName"]);
                    di.DeviceStatus = Convert.ToInt16(reader["DeviceStatus"]);
                    di.DeviceType = Convert.ToString(reader["DeviceType"]);
                    di.Remark = Convert.ToString(reader["Remark"]);
                    di.DeviceSpec = Convert.ToString(reader["DeviceSpec"]);
                    list.Add(di);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取指定类型设备的基本信息列表
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public static List<Model.DeviceInfo> GetDeviceInfoList(Model.DeviceType deviceType)
        {
            string type;
            switch (deviceType)
            {
                case Model.DeviceType.ROSO_RSJ:
                    type = "ROSO_RSJ";
                    break;
                case Model.DeviceType.ROSO_BZJ:
                    type = "ROSO_BZJ";
                    break;
                case Model.DeviceType.ROSO_DXJ:
                    type = "ROSO_DXJ";
                    break;
                case Model.DeviceType.ROSO_RDJ:
                    type = "ROSO_RDJ";
                    break;
                default:
                    return null;
            }
            string sql = "select * from ROSO_DeviceInfo where DeviceType=@DeviceType order by DeviceType,DeviceIP";
            SqlParameter[] p ={
                new SqlParameter("@DeviceType",type)
            };
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                List<Model.DeviceInfo> list = new List<Model.DeviceInfo>();
                while (reader.Read())
                {
                    Model.DeviceInfo di = new Model.DeviceInfo();
                    di.ID = Convert.ToInt32(reader["ID"]);
                    di.DeviceID = Convert.ToInt16(reader["DeviceID"]);
                    di.DeviceIP = Convert.ToString(reader["DeviceIP"]);
                    di.DeviceName = Convert.ToString(reader["DeviceName"]);
                    di.DeviceStatus = Convert.ToInt16(reader["DeviceStatus"]);
                    di.DeviceType = Convert.ToString(reader["DeviceType"]);
                    di.Remark = Convert.ToString(reader["Remark"]);
                    di.DeviceSpec = Convert.ToString(reader["DeviceSpec"]);
                    list.Add(di);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取设备的类型列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDeviceTypeList()
        {
            string sql = "select distinct(DeviceType) from ROSO_DeviceType";
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, null))
            {
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    list.Add(Convert.ToString(reader["DeviceType"]));
                }
                return list;
            }
        }

        /// <summary>
        /// 添加设备基本信息
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        public static bool AddDeviceInfo(Model.DeviceInfo deviceInfo)
        {
            string sql = "insert into ROSO_DeviceInfo(DeviceID,DeviceType,DeviceIP,DeviceSpec,DeviceName,DeviceStatus,Remark) values(@DeviceID,@DeviceType,@DeviceIP,@DeviceSpec,@DeviceName,@DeviceStatus,@Remark)";
            SqlParameter[] p ={
                    new SqlParameter("@DeviceID",deviceInfo.DeviceID),
                    new SqlParameter("@DeviceType",deviceInfo.DeviceType),
                    new SqlParameter("@DeviceIP",deviceInfo.DeviceIP),
                    new SqlParameter("@DeviceSpec",deviceInfo.DeviceSpec),
                    new SqlParameter("@DeviceName",deviceInfo.DeviceName),
                    new SqlParameter("@DeviceStatus",deviceInfo.DeviceStatus),
                    new SqlParameter("@Remark",deviceInfo.Remark)
                };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }

        /// <summary>
        /// 修改设备基本信息，编号不能修改
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        public static bool ChangeDeviceInfo(Model.DeviceInfo deviceInfo)
        {
            string sql = "update ROSO_DeviceInfo set DeviceType=@DeviceType,DeviceIP=@DeviceIP,DeviceSpec=@DeviceSpec,DeviceName=@DeviceName,DeviceStatus=@DeviceStatus,Remark=@Remark where DeviceID=@DeviceID";
            SqlParameter[] p ={
                    new SqlParameter("@DeviceID",deviceInfo.DeviceID),
                    new SqlParameter("@DeviceType",deviceInfo.DeviceType),
                    new SqlParameter("@DeviceIP",deviceInfo.DeviceIP),
                    new SqlParameter("@DeviceSpec",deviceInfo.DeviceSpec),
                    new SqlParameter("@DeviceName",deviceInfo.DeviceName),
                    new SqlParameter("@DeviceStatus",deviceInfo.DeviceStatus),
                    new SqlParameter("@Remark",deviceInfo.Remark)
            };
            int j = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return j > 0;
        }

        /// <summary>
        /// 删除设备基本信息
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public static bool DeleteDeviceInfo(Int16 deviceID)
        {
            string sql = "delete from ROSO_DeviceInfo where DeviceID=@DeviceID";
            SqlParameter[] p ={
                    new SqlParameter("@DeviceID",deviceID)
                };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }

        /// <summary>
        /// 判断设备的ID和IP是否已经存在
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="deviceIP"></param>
        /// <returns></returns>
        public static bool IDIPIsExist(Int16 deviceID, string deviceIP)
        {
            string sql = "select count(*) from ROSO_DeviceInfo where DeviceID=@DeviceID or DeviceIP=@DeviceIP";
            SqlParameter[] p = new SqlParameter[]
                    {
                        new SqlParameter("@DeviceID", deviceID),
                        new SqlParameter("@DeviceIP",deviceIP)
                    };
            int i = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, CommandType.Text, p));
            return i > 0;
        }

        /// <summary>
        /// 获取所有设备的数量
        /// </summary>
        /// <returns></returns>
        public static int GetDeviceCount()
        {
            string sql = "select count(*) from ROSO_DeviceInfo";
            int i = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, CommandType.Text, null));
            return i;
        }

        /// <summary>
        /// 获取指定类型设备的数量
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public static int GetDeviceCount(Model.DeviceType deviceType)
        {
            string type;
            switch (deviceType)
            {
                case Model.DeviceType.ROSO_RSJ:
                    type = "ROSO_RSJ";
                    break;
                case Model.DeviceType.ROSO_BZJ:
                    type = "ROSO_BZJ";
                    break;
                case Model.DeviceType.ROSO_DXJ:
                    type = "ROSO_DXJ";
                    break;
                case Model.DeviceType.ROSO_RDJ:
                    type = "ROSO_RDJ";
                    break;
                default:
                    return 0;
            }

            string sql = "select count(*) from ROSO_DeviceInfo where DeviceType=@DeviceType";
            SqlParameter[] p = new SqlParameter[]
                    {
                        new SqlParameter("@DeviceType",type)
                    };
            int i = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, CommandType.Text, p));
            return i;
        }

        /// <summary>
        /// 获取指定状态设备的基本信息列表
        /// </summary>
        /// <param name="deviceStatus"></param>
        /// <returns></returns>
        public static List<Model.DeviceInfo> GetDeviceInfoList(int deviceStatus)
        {
            string sql = "select * from ROSO_DeviceInfo where DeviceStatus=@DeviceStatus order by DeviceType,DeviceIP";
            SqlParameter[] p = new SqlParameter[]
                    {
                        new SqlParameter("@DeviceStatus",deviceStatus)
                    };
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                List<Model.DeviceInfo> list = new List<Model.DeviceInfo>();
                while (reader.Read())
                {
                    Model.DeviceInfo di = new Model.DeviceInfo();
                    di.ID = Convert.ToInt32(reader["ID"]);
                    di.DeviceID = Convert.ToInt16(reader["DeviceID"]);
                    di.DeviceIP = Convert.ToString(reader["DeviceIP"]);
                    di.DeviceName = Convert.ToString(reader["DeviceName"]);
                    di.DeviceStatus = Convert.ToInt16(reader["DeviceStatus"]);
                    di.DeviceType = Convert.ToString(reader["DeviceType"]);
                    di.Remark = Convert.ToString(reader["Remark"]);
                    di.DeviceSpec = Convert.ToString(reader["DeviceSpec"]);
                    list.Add(di);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取指定类型指定状态设备的基本信息列表
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="deviceStatus"></param>
        /// <returns></returns>
        public static List<Model.DeviceInfo> GetDeviceInfoList(Model.DeviceType deviceType,int deviceStatus)
        {
            string type;
            switch (deviceType)
            {
                case Model.DeviceType.ROSO_RSJ:
                    type = "ROSO_RSJ";
                    break;
                case Model.DeviceType.ROSO_BZJ:
                    type = "ROSO_BZJ";
                    break;
                case Model.DeviceType.ROSO_DXJ:
                    type = "ROSO_DXJ";
                    break;
                case Model.DeviceType.ROSO_RDJ:
                    type = "ROSO_RDJ";
                    break;
                default:
                    return null;
            }
            string sql = "select * from ROSO_DeviceInfo where DeviceType=@DeviceType and DeviceStatus=@DeviceStatus order by DeviceType,DeviceIP";
            SqlParameter[] p ={
                new SqlParameter("@DeviceType",type),
                new SqlParameter("@DeviceStatus",deviceStatus)
            };
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                List<Model.DeviceInfo> list = new List<Model.DeviceInfo>();
                while (reader.Read())
                {
                    Model.DeviceInfo di = new Model.DeviceInfo();
                    di.ID = Convert.ToInt32(reader["ID"]);
                    di.DeviceID = Convert.ToInt16(reader["DeviceID"]);
                    di.DeviceIP = Convert.ToString(reader["DeviceIP"]);
                    di.DeviceName = Convert.ToString(reader["DeviceName"]);
                    di.DeviceStatus = Convert.ToInt16(reader["DeviceStatus"]);
                    di.DeviceType = Convert.ToString(reader["DeviceType"]);
                    di.Remark = Convert.ToString(reader["Remark"]);
                    di.DeviceSpec = Convert.ToString(reader["DeviceSpec"]);
                    list.Add(di);
                }
                return list;
            }
        }




    }
}
