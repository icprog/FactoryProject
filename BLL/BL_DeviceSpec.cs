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
    /// 设备规格业务逻辑类
    /// </summary>
    public class BL_DeviceSpec
    {
        public static List<Model.DeviceSpec> listDeviceSpec;

        static BL_DeviceSpec()
        {
            listDeviceSpec = GetDeviceSpecList();
        }

        /// <summary>
        /// 根据ID获取设备规格信息，从静态列表中获取，效率高
        /// </summary>
        /// <param name="operatorID"></param>
        /// <returns></returns>
        public static string GetDeviceSpecValue(Int16 deviceSpecID)
        {
            Model.DeviceSpec ds = listDeviceSpec.Find(p => p.DeviceSpecID.Equals(deviceSpecID));
            return ds.DeviceSpecValue;
        }

        /// <summary>
        /// 根据ID获取设备规格对象，从数据库中获取
        /// </summary>
        public static Model.DeviceSpec GetDeviceSpec(Int16 deviceSpecID)
        {
            return DAL.DA_DeviceSpec.GetDeviceSpec(deviceSpecID);
        }

        /// <summary>
        /// 获取所有设备规格信息列表
        /// </summary>
        public static List<Model.DeviceSpec> GetDeviceSpecList()
        {
            return DAL.DA_DeviceSpec.GetDeviceSpecList();
        }

        /// <summary>
        /// 添加设备规格信息
        /// </summary>
        public static bool AddDeviceSpec(Model.DeviceSpec ds)
        {
            bool f= DAL.DA_DeviceSpec.AddDeviceSpec(ds);
            listDeviceSpec = GetDeviceSpecList();
            return f;
        }

        /// <summary>
        /// 删除设备规格信息
        /// </summary>
        public static bool DeleteDeviceSpecID(Int16 deviceSpecID)
        {
            bool f= DAL.DA_DeviceSpec.DeleteDeviceSpecID(deviceSpecID);
            listDeviceSpec = GetDeviceSpecList();
            return f;
        }

        /// <summary>
        /// 修改设备规格信息
        /// </summary>
        public static bool ChangeDeviceSpec(Model.DeviceSpec ds)
        {
            bool f= DAL.DA_DeviceSpec.ChangeDeviceSpec(ds);
            listDeviceSpec = GetDeviceSpecList();
            return f;
        }




    }
}
