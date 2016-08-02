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
    /// 绕带机对象，用于信息实时显示
    /// </summary>
    public class RDJ
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
        /// 横杆速度
        /// </summary>
        public Int16 HGSD { get; set; }
        /// <summary>
        /// 卷筒速度
        /// </summary>
        public Int16 JTSD { get; set; }
        /// <summary>
        /// 进料电机速度
        /// </summary>
        public Int16 JLDJSD { get; set; }
        /// <summary>
        /// 布带长度
        /// </summary>
        public Int16 BLCD { get; set; }
        /// <summary>
        /// 张力值
        /// </summary>
        public Int16 ZLZ { get; set; }
        /// <summary>
        /// 缓冲器高度
        /// </summary>
        public Int16 HCQGD { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public int CZRBH { get; set; }
        /// <summary>
        /// 卷筒编号
        /// </summary>
        public int JTBH { get; set; }
        /// <summary>
        /// 款号
        /// </summary>
        public int KH { get; set; }
        /// <summary>
        /// 最终长度
        /// </summary>
        public Int16 ZZCD { get; set; }

    }
}
