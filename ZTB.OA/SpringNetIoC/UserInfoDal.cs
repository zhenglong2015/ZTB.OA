using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetIoC
{
    class UserInfoDal : IUserInfoDal
    {
        public string Name { get; set; }
        public void Show()
        {
            Console.WriteLine("OK"+Name);
        }
    }
}
