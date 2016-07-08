// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/7/8 9:52:09
// Update Time          :    2016/7/8 9:52:09
// ==========================================
// Class Description     :    
// ==========================================
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongonDBDemo
{
    /// <summary>
    /// 数据库配置参数
    /// </summary>
    internal static class DbConfigParams
    {
        private static string _conntionString = ConfigurationManager.AppSettings["MongoDBConn"];

        /// <summary>
        /// 获取 数据库连接串
        /// </summary>
        public static string ConntionString
        {
            get { return _conntionString; }
        }

        private static string _dbName = ConfigurationManager.AppSettings["MongoDBName"];

        /// <summary>
        /// 获取 数据库名称
        /// </summary>
        public static string DbName
        {
            get { return _dbName; }
        }
    }
}
