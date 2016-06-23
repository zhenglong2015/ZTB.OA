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
                if (filterContext.HttpContext.Session["LoginUser"] == null)
                    filterContext.HttpContext.Response.Redirect("/Account/Index");
            }
            UserInfo = filterContext.HttpContext.Session["LoginUser"] as UserInfo;
        }
    }
}