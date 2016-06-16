// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/6/16 8:38:18
// Update Time          :    2016/6/16 8:38:18
// ==========================================
// Class Description     :    
// ==========================================
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ZTB.OA.DALFactory;
using ZTB.OA.EFDAL;
using ZTB.OA.IDAL;
using ZTB.OA.Model;

namespace ZTB.OA.DALFactory
{
    public class DbSessionFactory
    {
        public static IDbSession GetCurrentDbSession()
        {

            //一次请求共用一个实例
            IDbSession dbSession = CallContext.GetData("DbSession") as IDbSession;

            if (dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData("DbSession", dbSession);
            }
            return dbSession;
        }
    }
}
