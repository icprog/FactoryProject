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
    /// 染色机对象，用于信息实时显示
    /// </summary>
    public class RSJ
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
        /// 染缸数量
        /// </summary>
        public Int16 RGSL { get; set; }
        /// <summary>
        /// 设备运行状态1
        /// </summary>
        public Int16 SBYXZT1 { get; set; }
        /// <summary>
        /// 设备运行状态2
        /// </summary>
        public Int16 SBYXZT2 { get; set; }
        /// <summary>
        /// 设备运行状态3
        /// </summary>
        public Int16 SBYXZT3 { get; set; }
        /// <summary>
        /// 设备运行状态4
        /// </summary>
        public Int16 SBYXZT4 { get; set; }
        /// <summary>
        /// 设备运行状态5
        /// </summary>
        public Int16 SBYXZT5 { get; set; }
        /// <summary>
        /// 设备诊断信息
        /// </summary>
        public string SBZDXX { get; set; }
        /// <summary>
        /// 运行时间1
        /// </summary>
        public Int16 YXSJ1 { get; set; }
        /// <summary>
        /// 运行时间2
        /// </summary>
        public Int16 YXSJ2 { get; set; }
        /// <summary>
        /// 运行时间3
        /// </summary>
        public Int16 YXSJ3 { get; set; }
        /// <summary>
        /// 运行时间4
        /// </summary>
        public Int16 YXSJ4 { get; set; }
        /// <summary>
        /// 运行时间5
        /// </summary>
        public Int16 YXSJ5 { get; set; }
        /// <summary>
        /// 工艺段号1
        /// </summary>
        public Int16 GYDH1 { get; set; }
        /// <summary>
        /// 工艺段号2
        /// </summary>
        public Int16 GYDH2 { get; set; }
        /// <summary>
        /// 工艺段号3
        /// </summary>
        public Int16 GYDH3 { get; set; }
        /// <summary>
        /// 工艺段号4
        /// </summary>
        public Int16 GYDH4 { get; set; }
        /// <summary>
        /// 工艺段号5
        /// </summary>
        public Int16 GYDH5 { get; set; }
        /// <summary>
        /// 工艺步号1
        /// </summary>
        public Int16 GYBH1 { get; set; }
        /// <summary>
        /// 工艺步号2
        /// </summary>
        public Int16 GYBH2 { get; set; }
        /// <summary>
        /// 工艺步号3
        /// </summary>
        public Int16 GYBH3 { get; set; }
        /// <summary>
        /// 工艺步号4
        /// </summary>
        public Int16 GYBH4 { get; set; }
        /// <summary>
        /// 工艺步号5
        /// </summary>
        public Int16 GYBH5 { get; set; }
        /// <summary>
        /// 工艺名称1
        /// </summary>
        public int GYMC1 { get; set; }
        /// <summary>
        /// 工艺名称2
        /// </summary>
        public int GYMC2 { get; set; }
        /// <summary>
        /// 工艺名称3
        /// </summary>
        public int GYMC3 { get; set; }
        /// <summary>
        /// 工艺名称4
        /// </summary>
        public int GYMC4 { get; set; }
        /// <summary>
        /// 工艺名称5
        /// </summary>
        public int GYMC5 { get; set; }
        /// <summary>
        /// 泵速度1
        /// </summary>
        public Int16 BSD1 { get; set; }
        /// <summary>
        /// 泵速度2
        /// </summary>
        public Int16 BSD2 { get; set; }
        /// <summary>
        /// 泵速度3
        /// </summary>
        public Int16 BSD3 { get; set; }
        /// <summary>
        /// 泵速度4
        /// </summary>
        public Int16 BSD4 { get; set; }
        /// <summary>
        /// 泵速度5
        /// </summary>
        public Int16 BSD5 { get; set; }
        /// <summary>
        /// 温度1
        /// </summary>
        public Int16 WD1 { get; set; }
        /// <summary>
        /// 温度2
        /// </summary>
        public Int16 WD2 { get; set; }
        /// <summary>
        /// 温度3
        /// </summary>
        public Int16 WD3 { get; set; }
        /// <summary>
        /// 温度4
        /// </summary>
        public Int16 WD4 { get; set; }
        /// <summary>
        /// 温度5
        /// </summary>
        public Int16 WD5 { get; set; }
        /// <summary>
        /// 压力1
        /// </summary>
        public Int16 YL1 { get; set; }
        /// <summary>
        /// 压力2
        /// </summary>
        public Int16 YL2 { get; set; }
        /// <summary>
        /// 压力3
        /// </summary>
        public Int16 YL3 { get; set; }
        /// <summary>
        /// 压力4
        /// </summary>
        public Int16 YL4 { get; set; }
        /// <summary>
        /// 压力5
        /// </summary>
        public Int16 YL5 { get; set; }
        /// <summary>
        /// 控制字11
        /// </summary>
        public Int16 KZZ11 { get; set; }
        /// <summary>
        /// 控制字12
        /// </summary>
        public Int16 KZZ12 { get; set; }
        /// <summary>
        /// 控制字13
        /// </summary>
        public Int16 KZZ13 { get; set; }
        /// <summary>
        /// 控制字14
        /// </summary>
        public Int16 KZZ14 { get; set; }
        /// <summary>
        /// 控制字15
        /// </summary>
        public Int16 KZZ15 { get; set; }
        /// <summary>
        /// 控制字21
        /// </summary>
        public Int16 KZZ21 { get; set; }
        /// <summary>
        /// 控制字22
        /// </summary>
        public Int16 KZZ22 { get; set; }
        /// <summary>
        /// 控制字23
        /// </summary>
        public Int16 KZZ23 { get; set; }
        /// <summary>
        /// 控制字24
        /// </summary>
        public Int16 KZZ24 { get; set; }
        /// <summary>
        /// 控制字25
        /// </summary>
        public Int16 KZZ25 { get; set; }
        /// <summary>
        /// 控制字31
        /// </summary>
        public Int16 KZZ31 { get; set; }
        /// <summary>
        /// 控制字32
        /// </summary>
        public Int16 KZZ32 { get; set; }
        /// <summary>
        /// 控制字33
        /// </summary>
        public Int16 KZZ33 { get; set; }
        /// <summary>
        /// 控制字34
        /// </summary>
        public Int16 KZZ34 { get; set; }
        /// <summary>
        /// 控制字35
        /// </summary>
        public Int16 KZZ35 { get; set; }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public int CZRBH { get; set; }
        /// <summary>
        /// 卷筒编号1
        /// </summary>
        public int JTBH1 { get; set; }
        /// <summary>
        /// 卷筒编号2
        /// </summary>
        public int JTBH2 { get; set; }
        /// <summary>
        /// 卷筒编号3
        /// </summary>
        public int JTBH3 { get; set; }
        /// <summary>
        /// 卷筒编号4
        /// </summary>
        public int JTBH4 { get; set; }
        /// <summary>
        /// 卷筒编号5
        /// </summary>
        public int JTBH5 { get; set; }
    }
}
