using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTB.OA.Common.Logs
{
    public class NLogHelper : ILogManger
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }
    }
}
