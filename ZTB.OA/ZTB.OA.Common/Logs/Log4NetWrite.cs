// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/6/22 8:00:27
// Update Time          :    2016/6/22 8:00:27
// ==========================================
// Class Description     :    
// ==========================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ZTB.OA.Common.Logs
{
    public class Log4NetWrite : ILogWrite
    {
        public void WriteLogInfo(string str)
        {
            ILog logger = LogManager.GetLogger("Log4Net");
            logger.Error(str);
        }
    }
}
