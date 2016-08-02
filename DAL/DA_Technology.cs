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
    /// 工艺名称数据访问类
    /// </summary>
    public class DA_Technology
    {
        /// <summary>
        /// 根据ID获取工艺对象
        /// </summary>
        public static Model.Technology GetTechnology(Int32 technologyID)
        {
            string sql = "select * from ROSO_Technology where TechnologyID=@TechnologyID";
            SqlParameter[] p ={
                new SqlParameter("@TechnologyID",technologyID)
            };
            Model.Technology tech = new Model.Technology();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(sql, CommandType.Text, p))
            {
                if (dr.Read())
                {
                    tech.ID = Convert.ToInt32(dr["ID"]);
                    tech.TechnologyID = Convert.ToInt32(dr["TechnologyID"]);
                    tech.TechnologyValue = Convert.ToString(dr["TechnologyValue"]);
                }
            }
            return tech;
        }

        /// <summary>
        /// 获取所有工艺列表
        /// </summary>
        public static List<Model.Technology> GetTechnologyList()
        {
            string sql = "select * from ROSO_Technology order by TechnologyID";
            List<Model.Technology> list = new List<Model.Technology>();
            using (SqlDataReader dr = SQLHelper.ExecuteReader(sql, CommandType.Text, null))
            {
                while (dr.Read())
                {
                    Model.Technology tech = new Model.Technology();
                    tech.ID = Convert.ToInt32(dr["ID"]);
                    tech.TechnologyID = Convert.ToInt32(dr["TechnologyID"]);
                    tech.TechnologyValue = Convert.ToString(dr["TechnologyValue"]);
                    list.Add(tech);
                }
            }
            return list;
        }

        /// <summary>
        /// 添加工艺信息
        /// </summary>
        public static bool AddTechnology(Model.Technology tech)
        {
            string sql = "insert into ROSO_Technology(TechnologyID,TechnologyValue) values(@TechnologyID,@TechnologyValue)";
            SqlParameter[] p = {
                new SqlParameter("@TechnologyID",tech.TechnologyID),
                new SqlParameter("@TechnologyValue",tech.TechnologyValue)
                    };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }

        /// <summary>
        /// 删除工艺信息
        /// </summary>
        public static bool DeleteTechnology(Int32 TechnologyID)
        {
            string sql = "delete from ROSO_Technology where TechnologyID=@TechnologyID";
            SqlParameter[] p = {
                new SqlParameter("@TechnologyID",TechnologyID)
                    };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }

        /// <summary>
        /// 修改工艺信息
        /// </summary>
        public static bool ChangeTechnology(Model.Technology tech)
        {
            string sql = "update ROSO_Technology set TechnologyValue=@TechnologyValue where TechnologyID=@TechnologyID";
            SqlParameter[] p = {
                new SqlParameter("@TechnologyID",tech.TechnologyID),
                new SqlParameter("@TechnologyValue",tech.TechnologyValue)
                    };
            int i = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, p);
            return i > 0;
        }





    }
}
