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
            var users = UserInfoService.GetEntities(u => true).OrderByDescending(u => u.Id);

            int pageSize = 6;

            int pageNumber = (page ?? 1);

            return View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserInfo user)
        {
            user.ModifiedOn = DateTime.Now;
            user.ShowName = "测试";
            user.SubTime = DateTime.Now;

            UserInfoService.Add(user);
            return Content("ok");
        }

        public ActionResult Modify(int id)
        {
           var user= UserInfoService.GetEntities(u=>u.Id==id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult Modify(UserInfo user)
        {
            if (UserInfoService.Update(user))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            if (UserInfoService.Delete(user))
            {
                return Content("ok");
            }
            else
                return Content("no");
        }
    }
}