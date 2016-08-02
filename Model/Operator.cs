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
    /// 设备操作人实体类
    /// </summary>
    public class Operator
    {
        public int ID { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public int OperatorID { get; set; }
        private string operatorName;
        /// <summary>
        /// 操作人姓名，最长50个字符
        /// </summary>
        public string OperatorName
        {
            get { return operatorName; }
            set { operatorName = value.Length > 50 ? value.Substring(0, 50) : value; }
        }
    }
}
