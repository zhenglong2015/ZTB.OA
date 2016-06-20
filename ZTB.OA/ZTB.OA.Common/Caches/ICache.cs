using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTB.OA.Common.Caches
{
    public interface ICache
    {
        void Insert(string key, object obj, int expires = 20);
        object Get(string key);
        T Get<T>(string key);
    }
}
