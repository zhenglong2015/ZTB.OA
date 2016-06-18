using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetIoC
{
    public interface IUserInfoDal
    {
        string Name { get; set; }
        void Show();
    }
}
