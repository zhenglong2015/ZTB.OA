using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web;
using System.Threading;
using Spring.Context;
using Spring.Context.Support;

namespace ZTB.OA.Common.Caches
{
    /// <summary>
    /// 缓存辅助类
    /// </summary>
    public class CacheHelper
    {
        private static ICache CacheWriter { get; set; }

        static CacheHelper()
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            ctx.GetObject("CacheHelper");
        }
      
        public static void InsertCache(string key, object obj)
        {
            CacheWriter.Insert(key, obj);
        }

        public static object GetCache(string key)
        {
            return CacheWriter.Get(key);
        }

    }
}
