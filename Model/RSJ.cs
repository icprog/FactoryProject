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
    /// 染缸类
    /// </summary>
    public class RG
    {
        /// <summary>
        /// 染缸编号
        /// </summary>
        public Int16 RGBH { get; set; }

        /// <summary>
        /// 染缸诊断信息
        /// </summary>
        public string SBZDXX { get; set; }
        /// <summary>
        /// 设备运行状态
        /// </summary>
        public Int16 SBYXZT { get; set; }
        /// <summary>
        /// 运行时间
        /// </summary>
        public Int16 YXSJ { get; set; }
        /// <summary>
        /// 工艺段号
        /// </summary>
        public Int16 GYDH { get; set; }
        /// <summary>
        /// 工艺步号
        /// </summary>
        public Int16 GYBH { get; set; }
        /// <summary>
        /// 工艺名称
        /// </summary>
        public int GYMC { get; set; }
        /// <summary>
        /// 泵速度
        /// </summary>
        public Int16 BSD { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public Int16 WD { get; set; }
        /// <summary>
        /// 压力
        /// </summary>
        public Int16 YL { get; set; }
        /// <summary>
        /// 控制字1
        /// </summary>
        public string KZZ1 { get; set; }
        /// <summary>
        /// 控制字2
        /// </summary>
        public string KZZ2 { get; set; }
        /// <summary>
        /// 控制字3
        /// </summary>
        public string KZZ3 { get; set; }
        /// <summary>
        /// 卷筒编号
        /// </summary>
        public int JTBH { get; set; }
    }

    /// <summary>
    /// 染色机类，用于信息实时显示
    /// </summary>
    public class RSJ
    {
        /// <summary>
        /// 染缸集合
        /// </summary>
        public List<RG> RGS;
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
        /// 染缸数量
        /// </summary>
        public Int16 RGSL { get; set; }
        /// <summary>
        /// 设备诊断信息
        /// </summary>
        public string SBZDXX { get; set; }
        /// <summary>
        /// PLC状态信息
        /// </summary>
        public short[] PLCZT { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public int CZRBH { get; set; }
    }
}
