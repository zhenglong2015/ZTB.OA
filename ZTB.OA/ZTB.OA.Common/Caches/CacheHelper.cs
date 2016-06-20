using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web;
using System.Threading;

namespace ZTB.OA.Common.Caches
{
    /// <summary>
    /// 缓存辅助类
    /// </summary>
    public class CacheHelper
    {
        public static Queue<Dictionary<string, object>> cacheQueue = new Queue<Dictionary<string, object>>();

        public static HttpRuntimeCache hrc = new HttpRuntimeCache();

        static CacheHelper()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                lock (cacheQueue)
                {
                    //从队列中读取日志信息
                    Dictionary<string, object> dic = cacheQueue.Dequeue();
                    hrc.Insert(dic.Keys.First(), dic.Values.First());
                }
            });
        }
        public static void InsertCache(string key, object obj)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add(key, obj);
            cacheQueue.Enqueue(dic);
        }

        public static object GetCache(string key)
        {
            return hrc.Get(key);
        }

    }
}
