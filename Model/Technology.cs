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
    /// 工艺名称实体类
    /// </summary>
    public class Technology
    {
        public int ID { get; set; }
        /// <summary>
        /// 工艺编号
        /// </summary>
        public int TechnologyID { get; set; }

        private string technologyValue;
        /// <summary>
        /// 工艺名称，最长50个字符
        /// </summary>
        public string TechnologyValue
        {
            get { return technologyValue; }
            set { technologyValue = value.Length > 50 ? value.Substring(0, 50) : value; }
        }
    }
}
