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
    /// 设备基本信息的业务逻辑类
    /// </summary>
    public class BL_DeviceInfo
    {
        /// <summary>
        /// 按编号获取设备基本信息
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public static Model.DeviceInfo GetDeviceInfoByID(Int16 deviceID)
        {
            return DAL.DA_DeviceInfo.GetDeviceInfoByID(deviceID);
        }

        /// <summary>
        /// 按IP地址获取设备基本信息
        /// </summary>
        /// <param name="deviceIP"></param>
        /// <returns></returns>
        public static Model.DeviceInfo GetDeviceInfoByIP(string deviceIP)
        {
            return DAL.DA_DeviceInfo.GetDeviceInfoByIP(deviceIP);
        }

        /// <summary>
        /// 获取全部设备的基本信息列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.DeviceInfo> GetDeviceInfoList()
        {
            return DAL.DA_DeviceInfo.GetDeviceInfoList();
        }

        /// <summary>
        /// 获取指定类型设备的基本信息列表
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public static List<Model.DeviceInfo> GetDeviceInfoList(Model.DeviceType deviceType)
        {
            return DAL.DA_DeviceInfo.GetDeviceInfoList(deviceType);
        }

        /// <summary>
        /// 获取设备的类型列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDeviceTypeList()
        {
            return DAL.DA_DeviceInfo.GetDeviceTypeList();
        }

        /// <summary>
        /// 添加设备基本信息
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        public static bool AddDeviceInfo(Model.DeviceInfo deviceInfo)
        {
            return DAL.DA_DeviceInfo.AddDeviceInfo(deviceInfo);
        }

        /// <summary>
        /// 修改设备基本信息
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        public static bool ChangeDeviceInfo(Model.DeviceInfo deviceInfo)
        {
            return DAL.DA_DeviceInfo.ChangeDeviceInfo(deviceInfo);
        }

        /// <summary>
        /// 删除设备基本信息
        /// </summary>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public static bool DeleteDeviceInfo(Int16 deviceID)
        {
            return DAL.DA_DeviceInfo.DeleteDeviceInfo(deviceID);
        }

        /// <summary>
        /// 判断设备的ID和IP是否已经存在
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="deviceIP"></param>
        /// <returns></returns>
        public static bool IDIPIsExist(Int16 deviceID, string deviceIP)
        {
            return DAL.DA_DeviceInfo.IDIPIsExist(deviceID, deviceIP);
        }

        /// <summary>
        /// 获取所有设备的数量
        /// </summary>
        /// <returns></returns>
        public static int GetDeviceCount()
        {
            return DAL.DA_DeviceInfo.GetDeviceCount();
        }

        /// <summary>
        /// 获取指定类型设备的数量
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public static int GetDeviceCount(Model.DeviceType deviceType)
        {
            return DAL.DA_DeviceInfo.GetDeviceCount(deviceType);
        }

        /// <summary>
        /// 获取指定状态设备的基本信息列表
        /// </summary>
        /// <param name="deviceStatus">1:可用，2:停用</param>
        /// <returns></returns>
        public static List<Model.DeviceInfo> GetDeviceInfoList(int deviceStatus)
        {
            return DAL.DA_DeviceInfo.GetDeviceInfoList(deviceStatus);
        }

        /// <summary>
        /// 获取指定类型指定状态设备的基本信息列表
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="deviceStatus">1:可用，2:停用</param>
        /// <returns></returns>
        public static List<Model.DeviceInfo> GetDeviceInfoList(Model.DeviceType deviceType, int deviceStatus)
        {
            return DAL.DA_DeviceInfo.GetDeviceInfoList(deviceType, deviceStatus);
        }



    }
}
