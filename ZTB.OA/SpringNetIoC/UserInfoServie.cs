using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetIoC
{
    public class UserInfoServie
    {
        public IUserInfoDal UserInfoDal { get; set; }
        public void Show()
        {
          
            Console.WriteLine("UserInfoService");
        }
    }
}
