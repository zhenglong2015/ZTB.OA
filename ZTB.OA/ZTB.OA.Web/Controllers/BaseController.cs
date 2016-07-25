using System;
using System.Configuration;
using System.Web.Mvc;
using Spring.Context;
using Spring.Context.Support;
using ZTB.OA.IBLL;
using ZTB.OA.Model;
using System.Linq;
using ZTB.OA.BLL;

namespace ZTB.OA.Web.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsCheckLogin = true;
        public UserInfo UserInfo { get; set; }

        protected int PageSize
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]); }
        }
        /// <summary>
        /// 校验数据登录
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsCheckLogin)
            {
                if (Request.Cookies["LoginUser"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/Account/Login");
                    return;
                }
                string userId = Request.Cookies["LoginUser"].Value;
                UserInfo userInfo = Common.Caches.CacheHelper.GetCache(userId) as UserInfo;
                if (userInfo == null)
                {
                    //登录超时
                    filterContext.HttpContext.Response.Redirect("/Account/Login");
                    return;
                }
                UserInfo = userInfo;
                //滑动窗口机制
                Common.Caches.CacheHelper.InsertCache(userId, userInfo);

                #region 校验权限
                //校验权限
                if (UserInfo.Name == "admin")
                    return;

                //获取当前的请求地址
                string url = HttpContext.Request.RawUrl.ToLower();
                string method = HttpContext.Request.HttpMethod.ToLower();

                IApplicationContext ctx = ContextRegistry.GetContext();
                IActionInfoService actionInfoService = ctx.GetObject("ActionInfoService") as IActionInfoService;
                IRUserActionInfoService rUserActionInfoService = ctx.GetObject("RUserActionInfoService") as IRUserActionInfoService;
                IUserInfoService userInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;


                var actionInfo = actionInfoService.GetEntities(a => a.Url.ToLower() == url && a.HttpMethod.ToLower() == method).FirstOrDefault();

                if (actionInfo == null)
                {
                    filterContext.HttpContext.Response.Redirect("/401.html");
                    return;
                }
                var rUas = rUserActionInfoService.GetEntities(u => u.UserInfoId == UserInfo.Id);
                if (rUas == null)
                {
                    filterContext.HttpContext.Response.Redirect("/401.html");
                    return;
                }
                var item = (from a in rUas where a.ActionInfoId == actionInfo.Id select a).FirstOrDefault();

                if (item != null)
                {
                    if (item.HasPermission)
                        return;
                    else
                    {
                        filterContext.HttpContext.Response.Redirect("/401.html");
                        return;
                    }

                }

                //第2条线
                var user = userInfoService.GetEntities(u => u.Id == UserInfo.Id).FirstOrDefault();

                var allRole = from a in user.RoleInfo select a;

                var actions = from r in allRole from a in r.ActionInfo select a;

                var tem = (from t in actions where t.Id == actionInfo.Id select t).Count();

                if (tem <= 0)
                    filterContext.HttpContext.Response.Redirect("/401.html");
                #endregion
                base.OnActionExecuting(filterContext);
            }
        }
    }
}