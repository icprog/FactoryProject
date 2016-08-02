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

namespace ROSO.BLL
{
    /// <summary>
    /// 设备操作人业务类
    /// </summary>
    public class BL_Operator
    {
        public static List<Model.Operator> listOperator;

        static BL_Operator()
        {
            listOperator = GetOperatorList();
        }

        /// <summary>
        /// 根据ID获取操作人姓名，从静态列表中获取，效率高
        /// </summary>
        /// <param name="operatorID"></param>
        /// <returns></returns>
        public static string GetOperatorName(Int32 operatorID)
        {
            Model.Operator op = listOperator.Find(p => p.OperatorID.Equals(operatorID));
            return op.OperatorName;
        }

        /// <summary>
        /// 根据ID获取操作人对象，从数据库中获取
        /// </summary>
        public static Model.Operator GetOperator(Int32 operatorID)
        {
            return DAL.DA_Operator.GetOperator(operatorID);
        }

        /// <summary>
        /// 获取所有操作人列表
        /// </summary>
        public static List<Model.Operator> GetOperatorList()
        {
            return DAL.DA_Operator.GetOperatorList();
        }

        /// <summary>
        /// 添加设备操作人
        /// </summary>
        public static bool AddOperator(Model.Operator op)
        {
            bool f= DAL.DA_Operator.AddOperator(op);
            listOperator = GetOperatorList();
            return f;
        }

        /// <summary>
        /// 删除设备操作人
        /// </summary>
        public static bool DeleteOperator(Int32 operatorID)
        {
            bool f = DAL.DA_Operator.DeleteOperator(operatorID);
            listOperator = GetOperatorList();
            return f;
        }

        /// <summary>
        /// 修改设备操作人信息
        /// </summary>
        public static bool ChangeOperator(Model.Operator op)
        {
            bool f= DAL.DA_Operator.ChangeOperator(op);
            listOperator = GetOperatorList();
            return f;
        }
    }
}
