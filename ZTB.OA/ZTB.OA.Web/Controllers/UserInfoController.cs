using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.IBLL;
using ZTB.OA.Model;
using PagedList;

namespace ZTB.OA.Web.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        public IUserInfoService UserInfoService { get; set; }

        public ActionResult List(int? page)
        {
            var users = UserInfoService.GetEntities(u => true).OrderBy(u=>u.Id);

            int pageSize = 2;

            int pageNumber = (page ?? 1);

            return View(users.ToPagedList(pageNumber, pageSize));
        }
    }
}