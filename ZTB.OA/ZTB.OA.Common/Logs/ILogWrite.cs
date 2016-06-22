using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTB.OA.Common
{
    public interface ILogWrite
    {
        void Info(string message);
        void Error(string message);
    }
}
