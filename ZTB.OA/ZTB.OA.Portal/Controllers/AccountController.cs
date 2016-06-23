using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.BLL;
using ZTB.OA.Common;
using ZTB.OA.IBLL;
using ZTB.OA.Model;

namespace ZTB.OA.Portal.Controllers
{
    public class AccountController : Controller
    {
        public IUserInfoService UserInfoService { get; set; }
        // GET: Account
        public ActionResult Index()
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

        public ActionResult Login(string userName, string pwd, string vcode)
        {
            if (string.IsNullOrEmpty(vcode))
            {
                return Content("验证码有误！");
            }
            if (vcode != Session["Vcode"].ToString())
            {
                return Content("验证码有误！");
            }

            //  var user = UserInfoService.GetEntities(u => u.UName == userName && u.Pwd == pwd).FirstOrDefault();
            UserInfo user = null;
            if (user == null)
            {
                return RedirectToAction("Index", "UserInfo");
                return Content("用户名或密码错误！");
            }
            else
                return RedirectToAction("UserInfo", "Index");

        }
    }



}