using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.Model;

namespace ZTB.OA.Portal.Controllers
{
    public class BaseController : Controller
    {
        public bool IsCheckLogin = true;
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// 校验数据登录
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (IsCheckLogin)
            {

                if (Request.Cookies["LoginUser"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/Account/Index");
                    return;
                }
                string userId = Request.Cookies["LoginUser"].Value;
                UserInfo userInfo = Common.Caches.CacheHelper.GetCache(userId) as UserInfo;
                if (userInfo == null)
                {
                    //登录超时
                    filterContext.HttpContext.Response.Redirect("/Account/Index");
                    return;
                }
                UserInfo = userInfo;
                //滑动窗口机制
                Common.Caches.CacheHelper.InsertCache(userId, userInfo);
            }
        }
    }
}