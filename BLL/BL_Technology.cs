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
    /// 工艺信息业务类
    /// </summary>
    public class BL_Technology
    {
        public static List<Model.Technology> listTechnology;

        static BL_Technology()
        {
            listTechnology = GetTechnologyList();
        }

        /// <summary>
        /// 根据ID获取工艺名称，从静态列表中获取，效率高
        /// </summary>
        /// <param name="technologyID"></param>
        /// <returns></returns>
        public static string GetTechnologyValue(Int32 technologyID)
        {
            Model.Technology tech = listTechnology.Find(p => p.TechnologyID.Equals(technologyID));
            return tech.TechnologyValue;
        }

        /// <summary>
        /// 根据ID获取工艺对象，从数据库中获取
        /// </summary>
        public static Model.Technology GetTechnology(Int32 technologyID)
        {
            return DAL.DA_Technology.GetTechnology(technologyID);
        }

        /// <summary>
        /// 获取所有工艺列表
        /// </summary>
        public static List<Model.Technology> GetTechnologyList()
        {
            return DAL.DA_Technology.GetTechnologyList();
        }

        /// <summary>
        /// 添加工艺
        /// </summary>
        public static bool AddTechnology(Model.Technology tech)
        {
            bool f = DAL.DA_Technology.AddTechnology(tech);
            listTechnology = GetTechnologyList();
            return f;
        }

        /// <summary>
        /// 删除工艺
        /// </summary>
        public static bool DeleteTechnology(Int32 technologyID)
        {
            bool f = DAL.DA_Technology.DeleteTechnology(technologyID);
            listTechnology = GetTechnologyList();
            return f;
        }

        /// <summary>
        /// 修改工艺
        /// </summary>
        public static bool ChangeTechnology(Model.Technology tech)
        {
            bool f = DAL.DA_Technology.ChangeTechnology(tech);
            listTechnology = GetTechnologyList();
            return f;
        }
    }
}
