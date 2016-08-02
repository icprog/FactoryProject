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
    /// 设备状态的数据访问类
    /// </summary>
    public class DA_DeviceState
    {
        /// <summary>
        /// 判断今天是否有指定设备的状态信息
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="vatID"></param>
        /// <returns></returns>
        public static bool DeviceStateIsExist(Int16 deviceID,Int16 vatID)
        {
            string sql = "select Count(*) from ROSO_DeviceState where DeviceID=@DeviceID and VatID=@VatID and Convert(varchar(10),[DAQTime],120)=Convert(varchar(10),getDate(),120)";
            SqlParameter[] p ={
                new SqlParameter("@DeviceID",deviceID),
                new SqlParameter("@VatID",vatID)
            };
            int i = Convert.ToInt32( SQLHelper.ExecuteScalar(sql, CommandType.Text, p));
            return i > 0;
        }


        /// <summary>
        /// 获取指定设备的最新状态信息
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="vatID"></param>
        /// <returns></returns>
        public static Model.DeviceState GetDeviceState(Int16 deviceID, Int16 vatID)
        {
            string sql = "select * from ROSO_DeviceState where DAQTime=(select Max(DAQTime) from ROSO_DeviceState where DeviceID=@DeviceID and VatID=@VatID)";
            SqlParameter[] p ={
                new SqlParameter("@DeviceID",deviceID),
                new SqlParameter("@VatID",vatID)
            };
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                Model.DeviceState ds = new Model.DeviceState();
                if (reader.Read())
                {
                    ds.ID = Convert.ToInt32(reader["ID"]);
                    ds.DeviceID = Convert.ToInt16(reader["DeviceID"]);
                    ds.DAQTime = Convert.ToDateTime(reader["DAQTime"]);
                    ds.FaultMessage = Convert.ToString(reader["FaultMessage"]);
                    ds.OperatingState = Convert.ToInt16(reader["OperatingState"]);
                    ds.OperatorID = Convert.ToInt16(reader["OperatorID"]);
                    ds.VatID = Convert.ToInt16(reader["VatID"]);
                }
                return ds;
            }
        }

        /// <summary>
        /// 添加设备状态信息
        /// </summary>
        /// <param name="deviceState"></param>
        /// <returns></returns>
        public static bool AddDeviceState(Model.DeviceState deviceState)
        {
            string sql = "insert into ROSO_DeviceState(DeviceID,VatID,OperatingState,FaultMessage,DAQTime,OperatorID) values(@DeviceID,@VatID,@OperatingState,@FaultMessage,@DAQTime,@OperatorID)";
            SqlParameter[] p ={
                    new SqlParameter("@DeviceID",deviceState.DeviceID),
                    new SqlParameter("@VatID",deviceState.VatID),
                    new SqlParameter("@OperatingState",deviceState.OperatingState),
                    new SqlParameter("@FaultMessage",deviceState.FaultMessage),
                    new SqlParameter("@DAQTime",deviceState.DAQTime),
                    new SqlParameter("@OperatorID",deviceState.OperatorID)
                };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }

        /// <summary>
        /// 获取指定设备指定日期范围内的状态信息列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.DeviceState> GetDeviceStateList(Int16 deviceID,Int16 vatID, DateTime startDate, DateTime endDate)
        {
            string sql = "select * from ROSO_DeviceState where DeviceID=@DeviceID and VatID=@VatID and DAQTime>=@StartDate and DAQTime<=@EndDate order by DAQTime";
            SqlParameter[] p ={
                    new SqlParameter("@DeviceID",deviceID),
                    new SqlParameter("@VatID",vatID),
                    new SqlParameter("@StartDate",startDate),
                    new SqlParameter("@EndDate",endDate)
                };
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                List<Model.DeviceState> list = new List<Model.DeviceState>();
                while (reader.Read())
                {
                    Model.DeviceState ds = new Model.DeviceState();
                    ds.ID = Convert.ToInt32(reader["ID"]);
                    ds.DeviceID = Convert.ToInt16(reader["DeviceID"]);
                    ds.DAQTime = Convert.ToDateTime(reader["DAQTime"]);
                    ds.FaultMessage = Convert.ToString(reader["FaultMessage"]);
                    ds.OperatingState = Convert.ToInt16(reader["OperatingState"]);
                    ds.OperatorID = Convert.ToInt16(reader["OperatorID"]);
                    ds.VatID = Convert.ToInt16(reader["VatID"]);
                    list.Add(ds);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取全部设备指定日期范围内的状态信息列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.DeviceState> GetAllDeviceStateList(DateTime startDate, DateTime endDate)
        {
            string sql = "select * from ROSO_DeviceState where DAQTime>=@StartDate and DAQTime<=@EndDate order by DeviceID, DAQTime";
            SqlParameter[] p ={
                    new SqlParameter("@StartDate",startDate),
                    new SqlParameter("@EndDate",endDate)
                };
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                List<Model.DeviceState> list = new List<Model.DeviceState>();
                while (reader.Read())
                {
                    Model.DeviceState ds = new Model.DeviceState();
                    ds.ID = Convert.ToInt32(reader["ID"]);
                    ds.DeviceID = Convert.ToInt16(reader["DeviceID"]);
                    ds.DAQTime = Convert.ToDateTime(reader["DAQTime"]);
                    ds.FaultMessage = Convert.ToString(reader["FaultMessage"]);
                    ds.OperatingState = Convert.ToInt16(reader["OperatingState"]);
                    ds.OperatorID = Convert.ToInt16(reader["OperatorID"]);
                    ds.VatID = Convert.ToInt16(reader["VatID"]);
                    list.Add(ds);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取指定类型所有设备指定日期范围内的状态信息列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.DeviceState> GetDeviceStateListByDeviceType(Model.DeviceType deviceType, DateTime startDate, DateTime endDate)
        {
            string type;
            switch (deviceType)
            {
                case Model.DeviceType.ROSO_RSJ:
                    type="ROSO_RSJ";
                    break;
                case Model.DeviceType.ROSO_BZJ:
                    type="ROSO_BZJ";
                    break;
                case Model.DeviceType.ROSO_DXJ:
                    type="ROSO_DXJ";
                    break;
                case Model.DeviceType.ROSO_RDJ:
                    type="ROSO_RDJ";
                    break;
                default:
                    return null;
            }

            string sql = "select a.* from ROSO_DeviceState a,ROSO_DeviceInfo b where b.DeviceType=@DeviceType and b.DeviceID=a.DeviceID and a.DAQTime>=@StartDate and a.DAQTime<=@EndDate order by a.DeviceID, a.DAQTime";
            SqlParameter[] p ={
                    new SqlParameter("@DeviceType",type),
                    new SqlParameter("@StartDate",startDate),
                    new SqlParameter("@EndDate",endDate)
                };
            using (SqlDataReader reader = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                List<Model.DeviceState> list = new List<Model.DeviceState>();
                while (reader.Read())
                {
                    Model.DeviceState ds = new Model.DeviceState();
                    ds.ID = Convert.ToInt32(reader["ID"]);
                    ds.DeviceID = Convert.ToInt16(reader["DeviceID"]);
                    ds.DAQTime = Convert.ToDateTime(reader["DAQTime"]);
                    ds.FaultMessage = Convert.ToString(reader["FaultMessage"]);
                    ds.OperatingState = Convert.ToInt16(reader["OperatingState"]);
                    ds.OperatorID = Convert.ToInt16(reader["OperatorID"]);
                    ds.VatID = Convert.ToInt16(reader["VatID"]);
                    list.Add(ds);
                }
                return list;
            }
        }









    }
}
