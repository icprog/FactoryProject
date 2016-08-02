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
    /// 设备信息模板的业务逻辑类
    /// 设备的类型（DeviceType）：枚举
    /// </summary>
    public class BL_DeviceTemplate
    {

        /// <summary>
        /// 获取指定类型设备的模板信息列表
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public static List<Model.DeviceTemplate> GetDeviceTemplateList(Model.DeviceType deviceType)
        {
            return DAL.DA_DeviceTemplate.GetDeviceTemplateList(deviceType);
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
        /// 获取指定类型设备模板的项目列表
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public static List<string> GetDeviceTemplateProjectList(Model.DeviceType deviceType)
        {
            return DAL.DA_DeviceTemplate.GetDeviceTemplateProjectList(deviceType);
        }

        /// <summary>
        /// 在指定类型模板信息列表中获取指定项目的信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public static Model.DeviceTemplate GetDeviceTemplate(List<Model.DeviceTemplate> list, string project)
        {
            Model.DeviceTemplate dt = list.Find(p => p.Project.Equals(project));
            return dt;
        }

        /// <summary>
        /// 获取指定类型模板的字节总数
        /// </summary>
        /// <param name="deviceType"></param>
        /// <returns></returns>
        public static int GetDeviceTemplateByteCount(Model.DeviceType deviceType)
        {
            return DAL.DA_DeviceTemplate.GetDeviceTemplateByteCount(deviceType);
        }



        
    }
}
