using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZTB.OA.Common.Logs;

namespace ZTB.OA.Common.Logs
{
    public class LogHelper
    {
        public static Queue<ExceptionClass> ExceptionStringQueue = new Queue<ExceptionClass>();
        public static IList<ILogManger> logWriteList = new List<ILogManger>();

        static LogHelper()
        {
            //logWriteList.Add(new TextWrite());
            //logWriteList.Add(new SqlServerWrite());
            logWriteList.Add(LoggingFactory.GetDefaultLogger());

            ThreadPool.QueueUserWorkItem(o =>
            {
                lock (ExceptionStringQueue)
                {
                    if (ExceptionStringQueue.Count > 0)
                    {
                        //从队列中读取日志信息
                        ExceptionClass exc = ExceptionStringQueue.Dequeue();
                        foreach (var item in logWriteList)
                        {
                            if (exc.Type == ExceptionType.Error)
                                item.Error(exc.Message);
                            else
                                item.Info(exc.Message);
                        }
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
            });
        }
        public static void WriteErrorLog(string exceptionText)
        {
            ExceptionStringQueue.Enqueue(new ExceptionClass() { Type = ExceptionType.Error, Message = exceptionText });
        }

        public static void WriteInfoLog(string exceptionText)
        {
            ExceptionStringQueue.Enqueue(new ExceptionClass() { Type = ExceptionType.Info, Message = exceptionText });
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        public class ExceptionClass
        {
            public ExceptionType Type { get; set; }
            public string Message { get; set; }
        }
        /// <summary>
        /// 错误消息枚举
        /// </summary>
        public enum ExceptionType
        {
            Info,
            Error
        }
    }
}
