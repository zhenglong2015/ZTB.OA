using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Context;
using Spring.Context.Support;

namespace SpringNetIoC
{
    class Program
    {
        static void Main(string[] args)
        {
            //IUserInfoDal userInfodal = new UserInfoDal();
            //userInfodal.Show();
            IApplicationContext ctx = ContextRegistry.GetContext();
            //IUserInfoDal userInfoDal = (IUserInfoDal)ctx.GetObject("UserInfoDal");


            //IUserInfoDal userInfoDal = (IUserInfoDal)ctx.GetObject("UserInfoDal1");
            //userInfoDal.Show();


            UserInfoServie userInfoService = ctx.GetObject("UserInfoServie") as UserInfoServie;
            userInfoService.Show();
            Console.ReadKey();
        }


    }
}
