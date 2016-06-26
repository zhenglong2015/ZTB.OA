using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.Common;
using ZTB.OA.IBLL;
using ZTB.OA.Model;

namespace ZTB.OA.Web.Controllers
{
    public class AccountController : Controller
    {
        public IUserInfoService UserInfoService { get; set; }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult CreateValidateCode()
        {
            ValidateCode vc = new ValidateCode();
            Bitmap map = vc.CreateValidateCode();

            MemoryStream stream = new MemoryStream();
            map.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Seek(0, 0);

            Session["Vcode"] = vc.strValidateCode;

            return File(stream.ToArray(), @"image/jpeg");
        }
        [HttpPost]
        public ActionResult Login(string userName, string pwd, string vcode)
        {
            if (string.IsNullOrEmpty(vcode) || Session["Vcode"] == null)
            {
                return Content("验证码有误！");
            }
            if (vcode != Session["Vcode"].ToString())
            {
                return Content("验证码有误！");
            }
            Session["Vcode"] = null;
            var user = UserInfoService.GetEntities(u => u.UName == userName && u.Pwd == pwd).FirstOrDefault();
            if (user == null)
                return Content("用户名或密码错误！");
            else
            {
                string userId = Guid.NewGuid().ToString();
                Common.Caches.CacheHelper.InsertCache(userId, user);

                Response.Cookies["LoginUser"].Value = userId;//网客户端写Cookie
                return Content("Ok");
            }
        }
    }
}