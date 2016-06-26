using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    class Program
    {
        public static IRedisClientsManager clientManager = new PooledRedisClientManager(new string[] { "127.0.0.1:6379" });
        public static IRedisClient redisClent = clientManager.GetClient();
    
        static void Main(string[] args)
        {
            var client = new RedisClient("127.0.0.1", 6379);

            #region 字符串类型
            client.Set<string>("name", "laowang");
            string userName = client.Get<string>("name");
            Console.WriteLine(userName);
            Console.ReadKey();
            #endregion


            client.SetEntryInHash("userInfoId", "name", "zhangsan");
            client.GetHashKeys("userInfoId");
            client.GetHashValues("userInfoId");

            //队列.
            client.EnqueueItemOnList("name2", "laowang");//入队。
            client.EnqueueItemOnList("name2", "laoma");
            long length = client.GetListCount("name2");
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(client.DequeueItemFromList("name2"));//出队.
            }

            client.PushItemToList("name1", "laowang");//入栈
            client.PushItemToList("name1", "laoma");
            long length1 = client.GetListCount("name1");
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(client.PopItemFromList("name1"));//出栈.
            }


            Console.ReadKey();
        }
    }
}
