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

namespace ROSO.Model
{
    /// <summary>
    /// 设备基本信息类
    /// </summary>
    public class DeviceInfo
    {
        public int ID { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public Int16 DeviceID { get; set; }

        private string deviceType;
        /// <summary>
        /// 设备类型，最长50字符
        /// </summary>
        public string DeviceType
        {
            get { return deviceType; }
            set { deviceType = value.Length > 50 ? value.Substring(0, 50) : value; }
        }

        private string deviceIP;
        /// <summary>
        /// 设备的IP，最长50字符
        /// </summary>
        public string DeviceIP
        {
            get { return deviceIP; }
            set { deviceIP = value.Length > 50 ? value.Substring(0, 50) : value; }
        }

        private string deviceSpec;
        /// <summary>
        /// 设备规格，最长50字符
        /// </summary>
        public string DeviceSpec
        {
            get { return deviceSpec; }
            set { deviceSpec = value.Length > 50 ? value.Substring(0, 50) : value; }
        }

        private string deviceName;
        /// <summary>
        /// 设备名称，最长50字符
        /// </summary>
        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value.Length > 50 ? value.Substring(0, 50) : value; }
        }

        /// <summary>
        /// 设备的状态：可用0、停用1
        /// </summary>
        public Int16 DeviceStatus { get; set; }

        private string remark;
        /// <summary>
        /// 备注，最长50字符
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value.Length > 50 ? value.Substring(0, 50) : value; }
        }
    }
}
