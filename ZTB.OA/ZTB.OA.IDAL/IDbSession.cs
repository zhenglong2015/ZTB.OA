// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/6/16 8:26:47
// Update Time          :    2016/6/16 8:26:47
// ==========================================
// Class Description     :    
// ==========================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTB.OA.IDAL
{
    public interface IDbSession
    {
        IUserInfoDal UserInfoDal { get; }

        int SaveChanges();
    }
}
