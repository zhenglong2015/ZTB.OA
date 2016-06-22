using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZTB.OA.Common.Logs;

namespace ZTB.OA.Common
{
    public class LogHelper
    {
        public static Queue<string> ExceptionStringQueue = new Queue<string>();
        public static List<ILogWrite> logWriteList = new List<ILogWrite>();

        static LogHelper()
        {
            //logWriteList.Add(new TextWrite());
            //logWriteList.Add(new SqlServerWrite());
            logWriteList.Add(new Log4NetWrite());

            ThreadPool.QueueUserWorkItem(o =>
            {
                lock (ExceptionStringQueue)
                {
                    if (ExceptionStringQueue.Count > 0)
                    {
                        //从队列中读取日志信息
                        string str = ExceptionStringQueue.Dequeue();
                        foreach (var item in logWriteList)
                        {
                            item.WriteLogInfo(str);
                        }
                    }
                    else
                    {
                        Thread.Sleep(30);
                    }
                }
            });
        }
        public static void WriteLog(string exceptionText)
        {
            ExceptionStringQueue.Enqueue(exceptionText);
        }
    }
}
