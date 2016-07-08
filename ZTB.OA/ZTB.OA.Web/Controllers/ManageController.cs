using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.IBLL;

namespace ZTB.OA.Web.Controllers
{
    public class ManageController : BaseController
    {

        public IUserInfoService UserInfoService { get; set; }
        // 修改头像
        public ActionResult ModifyHead()
        {
            return View();
        }
        //个人信息
        public ActionResult UseProfile()
        {
            return View();
        }

        public ActionResult ModifyPwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ModifyPwd(string oldPwd, string newPwd)
        {
            if (!string.IsNullOrEmpty(oldPwd) && oldPwd == base.UserInfo.Pwd)
            {
                string userId = Request.Cookies["LoginUser"].Value;

                var user = UserInfoService.GetEntities(u => u.Id == UserInfo.Id).FirstOrDefault();
                user.Pwd = newPwd;
                UserInfoService.Update(user);
                Response.Cookies["LoginUser"].Value = null;
                return Content("ok");

            }
            else
            {
                return Content("原密码输入错误");
            }
        }


        //联系我们
        public ActionResult Contacts()
        {
            return View();
        }
    }
}