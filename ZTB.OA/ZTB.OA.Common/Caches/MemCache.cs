using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTB.OA.Common.Caches
{
    public class MemCache : ICache
    {
        public MemcachedClient memClient;
        public MemCache()
        {
            // Memcached服务器列表
            // 如果有多台服务器，则以逗号分隔，例如："192.168.80.10:11211","192.168.80.11:11211"
            var str = ConfigurationManager.AppSettings["MemcachedServers"];
            if (!string.IsNullOrEmpty(str))
            {
                string[] serverList = ConfigurationManager.AppSettings["MemcachedServers"].Split(',');
                // 初始化SocketIO池
                string poolName = "MyPool";
                SockIOPool sockIOPool = SockIOPool.GetInstance(poolName);
                // 添加服务器列表
                sockIOPool.SetServers(serverList);
                // 设置连接池初始数目
                sockIOPool.InitConnections = 3;
                // 设置连接池最小连接数目
                sockIOPool.MinConnections = 3;
                // 设置连接池最大连接数目
                sockIOPool.MaxConnections = 5;
                // 设置连接的套接字超时时间（单位：毫秒）
                sockIOPool.SocketConnectTimeout = 1000;
                // 设置套接字超时时间（单位：毫秒）
                sockIOPool.SocketTimeout = 3000;
                // 设置维护线程运行的睡眠时间：如果设置为0，那么维护线程将不会启动
                sockIOPool.MaintenanceSleep = 30;
                // 设置SockIO池的故障标志
                sockIOPool.Failover = true;
                // 是否用nagle算法启动
                sockIOPool.Nagle = false;
                // 正式初始化容器
                sockIOPool.Initialize();

                // 获取Memcached客户端实例
                memClient = new MemcachedClient();
                // 指定客户端访问的SockIO池
                memClient.PoolName = poolName;
                // 是否启用压缩数据：如果启用了压缩，数据压缩长于门槛的数据将被储存在压缩的形式
                memClient.EnableCompression = false;
            }
        }


        public object Get(string key)
        {
            return memClient.Get(key);
        }

        public void Insert(string key, object obj, int expires = 20)
        {
            memClient.Set(key, obj);
        }
    }
}
