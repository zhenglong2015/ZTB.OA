using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZTB.OA.EFDAL;
using ZTB.OA.IDAL;

namespace ZTB.OA.DALFactory
{
    public class StaticFactory
    {
        public static IUserInfoDal GetDal()
        {
            // return new UserInfoDal();
            string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoDal") as IUserInfoDal;
        }
    }
}
