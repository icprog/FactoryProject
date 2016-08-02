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
    /// 包装机对象，用于信息实时显示
    /// </summary>
    public class BZJ
    {
        /// <summary>
        /// 设备型号
        /// </summary>
        public Int16 SBXH { get; set; }
        /// <summary>
        /// 设备规格
        /// </summary>
        public Int16 SBGG { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public Int16 SBBH { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        public Int16 BY { get; set; }
        /// <summary>
        /// 设备运行状态
        /// </summary>
        public Int16 SBYXZT { get; set; }
        /// <summary>
        /// 设备诊断信息
        /// </summary>
        public string SBZDXX { get; set; }
        /// <summary>
        /// PLC状态信息
        /// </summary>
        public short[] PLCZT { get; set; }
        /// <summary>
        /// 布带速度
        /// </summary>
        public Int16 BDSD { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public int CZRBH { get; set; }
        /// <summary>
        /// 卷筒编号
        /// </summary>
        public Int16 JTBH { get; set; }
        /// <summary>
        /// 布带长度
        /// </summary>
        public Int16 BDCD { get; set; }
        /// <summary>
        /// 容器编号
        /// </summary>
        public Int16 RQBH { get; set; }
        /// <summary>
        /// 接头数量
        /// </summary>
        public Int16 JTSL { get; set; }
    }
}
