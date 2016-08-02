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

namespace ROSO.BLL
{
    /// <summary>
    /// 设备状态的业务逻辑类
    /// </summary>
    public class BL_DeviceState
    {
        /// <summary>
        /// 添加设备状态信息：
        /// 每次获取到设备的实时状态信息，就产生一个DeviceState对象，并调用此方法添加设备状态变化信息；
        /// 先判断当前状态与最近状态是否有变化，
        /// 再判断今天是否已有状态信息，
        /// 若已有且无变化，不添加；
        /// 若状态有变化，或今天还没有状态信息，添加状态信息。
        /// </summary>
        /// <param name="deviceState"></param>
        /// <returns></returns>
        public static bool AddDeviceState(Model.DeviceState deviceState)
        {
            if (deviceState.OperatingState != DAL.DA_DeviceState.GetDeviceState(deviceState.DeviceID,deviceState.VatID).OperatingState || !DAL.DA_DeviceState.DeviceStateIsExist(deviceState.DeviceID,deviceState.VatID))
            {
                return DAL.DA_DeviceState.AddDeviceState(deviceState);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加设备整点状态信息
        /// </summary>
        /// <param name="deviceState">设备状态</param>
        /// <param name="time">整点时间</param>
        /// <returns></returns>
        public static bool AddDeviceState(Model.DeviceState deviceState,DateTime dt)
        {
            deviceState.DAQTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
            return DAL.DA_DeviceState.AddDeviceState(deviceState);
        }


        /// <summary>
        /// 获取指定设备指定日期范围内的状态信息列表
        /// </summary>
        /// <param name="deviceID">设备编号</param>
        /// <param name="vatID">染缸编号，染缸用1,2,3,4,5，其它设备用0</param>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static List<Model.DeviceState> GetDeviceStateList(Int16 deviceID,Int16 vatID, DateTime startDate, DateTime endDate)
        {
            return DAL.DA_DeviceState.GetDeviceStateList(deviceID,vatID, startDate, endDate);
        }

        /// <summary>
        /// 获取指定设备指定日期范围内的状态信息列表
        /// </summary>
        /// <param name="deviceID">设备IP</param>
        /// <param name="vatID">染缸编号，染缸用1,2,3,4,5，其它设备用0</param>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static List<Model.DeviceState> GetDeviceStateList(string deviceIP,Int16 vatID, DateTime startDate, DateTime endDate)
        {
            Int16 deviceID = BLL.BL_DeviceInfo.GetDeviceInfoByIP(deviceIP).DeviceID;
            return DAL.DA_DeviceState.GetDeviceStateList(deviceID,vatID, startDate, endDate);
        }

        /// <summary>
        /// 获取全部设备指定日期范围内的状态信息列表
        /// </summary>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static List<Model.DeviceState> GetAllDeviceStateList(DateTime startDate, DateTime endDate)
        {
            return DAL.DA_DeviceState.GetAllDeviceStateList(startDate, endDate);
        }

        /// <summary>
        /// 获取指定类型所有设备指定日期范围内的状态信息列表
        /// </summary>
        /// <param name="deviceType">设备类型枚举</param>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public static List<Model.DeviceState> GetDeviceStateListByDeviceType(Model.DeviceType deviceType, DateTime startDate, DateTime endDate)
        {
            return DAL.DA_DeviceState.GetDeviceStateListByDeviceType(deviceType, startDate, endDate);
        }

        /// <summary>
        /// 获取指定设备指定日期范围内指定状态的持续时间
        /// </summary>
        /// <param name="deviceID">设备编号</param>
        /// <param name="vatID">染缸编号，染缸用1,2,3,4,5，其它设备用0</param>
        /// <param name="startDate">起始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="deviceState">设备状态，可以是1,2,3,4</param>
        /// <returns></returns>
        public static double GetDeviceStateDuration( Int16 deviceID, Int16 vatID, DateTime startDate, DateTime endDate,Int16 operatingState)
        {
            List<Model.DeviceState> list= DAL.DA_DeviceState.GetDeviceStateList(deviceID, vatID, startDate, endDate);
            double t = 0;
            if (list.Count > 1)
            {
                bool f = false;
                DateTime dt=list[0].DAQTime;
                for (int i = 0; i < list.Count; i++)
                {
                    //状态值10以上相当于1，都属于运行中
                    if ((list[i].OperatingState == operatingState) || (operatingState == 1 && list[i].OperatingState > 10))
                    {
                        if (f)
                        {
                            //累加本时间段
                            TimeSpan ts = list[i].DAQTime - dt;
                            t = t + ts.TotalHours;
                        }
                        //新的起始时间
                        dt = list[i].DAQTime;
                        f = true;
                    }
                    else
                    {
                        if (f)
                        {
                            //累加本时间段
                            TimeSpan ts = list[i].DAQTime - dt;
                            t = t + ts.TotalHours;
                        }
                        f = false;
                    }
                }
            }
            return t;
        }








    }
}
