using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTB.OA.EFDAL;
using ZTB.OA.IDAL;

namespace ZTB.OA.DALFactory
{
   public class DbSession: IDbSession
    {
        public IUserInfoDal UserInfoDal
        {
            get { return StaticFactory.GetUserInfoDal(); }
        }

        public int SaveChanges()
        {
           return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }
    }
}
