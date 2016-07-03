﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTB.OA.Model;
using ZTB.OA.Model.Param;

namespace ZTB.OA.IBLL
{
    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
        IQueryable<UserInfo> LoagPageData(UserQueryParam queryParam);
    }
}