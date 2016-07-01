using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.IBLL;
using ZTB.OA.Model;

namespace ZTB.OA.Web.Controllers
{
    public class UserInfoController : BaseController
    {
        // GET: UserInfo
       public IUserInfoService UserInfoService { get; set; }
    }
}