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
using System.Data.SqlClient;
using System.Data;

namespace ROSO.DAL
{
    /// <summary>
    /// 设备操作人数据访问类
    /// </summary>
    public class DA_Operator
    {
        /// <summary>
        /// 根据ID获取操作人
        /// </summary>
        public static Model.Operator GetOperator(Int32 operatorID)
        {
            string sql = "select * from ROSO_Operator where OperatorID=@OperatorID";
            SqlParameter[] p ={
                new SqlParameter("@OperatorID",operatorID)
            };
            Model.Operator op = new Model.Operator();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                if (dr.Read())
                {
                    op.ID = Convert.ToInt32(dr["ID"]);
                    op.OperatorID = Convert.ToInt32(dr["OperatorID"]);
                    op.OperatorName = Convert.ToString(dr["OperatorName"]);
                }
            }
            return op;
        }

        /// <summary>
        /// 获取所有操作人列表
        /// </summary>
        public static List<Model.Operator> GetOperatorList()
        {
            string sql = "select * from ROSO_Operator order by OperatorID";
            List<Model.Operator> list = new List<Model.Operator>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(sql, CommandType.Text, null))
            {
                while (dr.Read())
                {
                    Model.Operator op = new Model.Operator();
                    op.ID = Convert.ToInt32(dr["ID"]);
                    op.OperatorID = Convert.ToInt32(dr["OperatorID"]);
                    op.OperatorName = Convert.ToString(dr["OperatorName"]);
                    list.Add(op);
                }
            }
            return list;
        }

        /// <summary>
        /// 添加设备操作人
        /// </summary>
        public static bool AddOperator(Model.Operator op)
        {
            string sql = "insert into ROSO_Operator(OperatorID,OperatorName) values(@OperatorID,@OperatorName)";
            SqlParameter[] p = {
                new SqlParameter("@OperatorID",op.OperatorID),
                new SqlParameter("@OperatorName",op.OperatorName)
                    };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }

        /// <summary>
        /// 删除设备操作人
        /// </summary>
        public static bool DeleteOperator(Int32 operatorID)
        {
            string sql = "delete from ROSO_Operator where OperatorID=@OperatorID";
            SqlParameter[] p = {
                new SqlParameter("@OperatorID",operatorID)
                    };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }

        /// <summary>
        /// 修改设备操作人信息
        /// </summary>
        public static bool ChangeOperator(Model.Operator op)
        {
            string sql = "update ROSO_Operator set OperatorName=@OperatorName where OperatorID=@OperatorID";
            SqlParameter[] p = {
                new SqlParameter("@OperatorID",op.OperatorID),
                new SqlParameter("@OperatorName",op.OperatorName)
                    };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }





    }
}
