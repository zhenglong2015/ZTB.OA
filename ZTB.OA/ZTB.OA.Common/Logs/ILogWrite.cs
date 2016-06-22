using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTB.OA.Common.Logs
{
    /// <summary>
    /// 日志记录依赖接口
    /// 替换为其他记录日志方式是需实现此接口
    /// </summary>
    public interface ILogWrite
    {
        //一般信息
        void Info(string message);
        //错误信息
        void Error(string message);
    }
}
