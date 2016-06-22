using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.IBLL;
using ZTB.OA.Model;

namespace ZTB.OA.Portal.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
       public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
            ViewData.Model = UserInfoService.GetEntities(t => true).ToList();
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            UserInfoService.Add(userInfo);
            return RedirectToAction("Index");
        }
    }
}