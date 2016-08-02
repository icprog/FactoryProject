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
    /// 设备状态类，记录设备的运行状态
    /// </summary>
    public class DeviceState
    {
        public int ID { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public Int16 DeviceID { get; set; }
        /// <summary>
        /// 染色机染缸编号，值为1，2，3，4，5；其它设备没有染缸，统一用0表示
        /// </summary>
        public Int16 VatID { get; set; }
        /// <summary>
        /// 设备的当前运行状态，用int值表示
        /// </summary>
        public Int16 OperatingState { get; set; }

        private string faultMessage;
        /// <summary>
        /// 设备的当前错误信息，最长200字符
        /// </summary>
        public string FaultMessage
        {
            get { return faultMessage; }
            set { faultMessage = value.Length > 200 ? value.Substring(0, 200) : value; }
        }
        /// <summary>
        /// 数据采集时间
        /// </summary>
        public DateTime DAQTime { get; set; }
        /// <summary>
        /// 设备操作人编号
        /// </summary>
        public Int16 OperatorID { get; set; }
    }
}
