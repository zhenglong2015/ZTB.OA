using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZTB.OA.Web.Controllers
{
    public class ManageController : Controller
    {
        // 修改头像
        public ActionResult ModifyHead()
        {
            return View();
        }
        //个人信息
        public ActionResult Profile()
        {
            return View();
        }
        //联系我们
        public ActionResult Contacts()
        {
            return View();
        }
    }
}