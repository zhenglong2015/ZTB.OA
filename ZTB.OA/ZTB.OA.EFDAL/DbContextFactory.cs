using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using ZTB.OA.Model;

namespace ZTB.OA.EFDAL
{
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {

            //一次请求共用一个实例
            DbContext db = CallContext.GetData("DataModelContainer") as DbContext;

            if (db == null)
            {
                db = new DataModelContainer();
                CallContext.SetData("DataModelContainer", db);
            }
            return db;
        }
    }
}
