using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ZTB.OA.Web.Models
{
    public class WebWxConfigHelper
    {
        //微信配置
        public static string appid = ConfigurationManager.AppSettings["appid"];
        public static string mch_id = ConfigurationManager.AppSettings["mchid"];
        public static string domainUrl = ConfigurationManager.AppSettings["url"];
        public static string anyscUrl = ConfigurationManager.AppSettings["anyscurl"];
    }
}