﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     如需对单个文件扩展请使用部分类，否则当文本模板文件再次保存时将把修改冲刷掉
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZTB.OA.DALFactory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ZTB.OA.EFDAL;
    using ZTB.OA.IDAL;
    
     public partial class DbSession :IDbSession
    {   
    		
    		 public IActionInfoDal ActionInfoDal
            {
                get
                {
                    return StaticFactory.GetActionInfoDal();
                }
            }       
    		
    		 public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
            {
                get
                {
                    return StaticFactory.GetR_UserInfo_ActionInfoDal();
                }
            }       
    		
    		 public IRoleInfoDal RoleInfoDal
            {
                get
                {
                    return StaticFactory.GetRoleInfoDal();
                }
            }       
    		
    		 public IUserInfoDal UserInfoDal
            {
                get
                {
                    return StaticFactory.GetUserInfoDal();
                }
            }       
    		 public int SaveChanges()
            {
               return DbContextFactory.GetCurrentDbContext().SaveChanges();
            }
    }
}
