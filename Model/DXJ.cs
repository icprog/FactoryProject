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
    /// 定型机对象，用于信息实时显示
    /// </summary>
    public class DXJ
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
        /// 反转马达速度
        /// </summary>
        public Int16 FZMDSD { get; set; }
        /// <summary>
        /// 第一级速度
        /// </summary>
        public Int16 SD1 { get; set; }
        /// <summary>
        /// 第二级速度
        /// </summary>
        public Int16 SD2 { get; set; }
        /// <summary>
        /// 第三级速度
        /// </summary>
        public Int16 SD3 { get; set; }
        /// <summary>
        /// 第四级速度
        /// </summary>
        public Int16 SD4 { get; set; }
        /// <summary>
        /// 第五级速度
        /// </summary>
        public Int16 SD5 { get; set; }
        /// <summary>
        /// 循环马达速度
        /// </summary>
        public Int16 XHMDSD { get; set; }
        /// <summary>
        /// 出口马达速度
        /// </summary>
        public Int16 CKMDSD { get; set; }
        /// <summary>
        /// 鲜风马达速度
        /// </summary>
        public Int16 XFMDSD { get; set; }
        /// <summary>
        /// 上左温度
        /// </summary>
        public Int16 ULWD { get; set; }
        /// <summary>
        /// 上中温度
        /// </summary>
        public Int16 UMWD { get; set; }
        /// <summary>
        /// 上右温度
        /// </summary>
        public Int16 URWD { get; set; }
        /// <summary>
        /// 下左温度
        /// </summary>
        public Int16 DLWD { get; set; }
        /// <summary>
        /// 下中温度
        /// </summary>
        public Int16 DMWD { get; set; }
        /// <summary>
        /// 下右温度
        /// </summary>
        public Int16 DRWD { get; set; }
        /// <summary>
        /// 炉内温度1
        /// </summary>
        public Int16 LNWD1 { get; set; }
        /// <summary>
        /// 炉内温度2
        /// </summary>
        public Int16 LNWD2 { get; set; }
        /// <summary>
        /// 加热部温度
        /// </summary>
        public Int16 JRBWD { get; set; }
        /// <summary>
        /// 出口温度
        /// </summary>
        public Int16 CKWD { get; set; }
        /// <summary>
        /// 交换部温度
        /// </summary>
        public Int16 JHBWD { get; set; }
        /// <summary>
        /// 加热功率
        /// </summary>
        public Int16 JRGL { get; set; }

    }
}
