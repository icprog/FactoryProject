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
    /// 设备规格信息实体类
    /// </summary>
    public class DeviceSpec
    {
        public int ID { get; set; }
        /// <summary>
        /// 设备规格编码
        /// </summary>
        public int DeviceSpecID { get; set; }
        private string deviceSpecValue;
        /// <summary>
        /// 设备规格，最长50个字符
        /// </summary>
        public string DeviceSpecValue
        {
            get { return deviceSpecValue; }
            set { deviceSpecValue = value.Length > 50 ? value.Substring(0, 50) : value; }
        }
    }
}
