using System;
using System.Configuration;
using System.Web.Mvc;
using ZTB.OA.Model;

namespace ZTB.OA.Web.Controllers
{
    public class BaseController : Controller
    {
        public bool IsCheckLogin = true;
        public UserInfo UserInfo { get; set; }

        public int PageSize
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]); }
        }

        /// <summary>
        /// 校验数据登录
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string backUrl = filterContext.HttpContext.Request.RawUrl;
            string loginUrl = string.Format("/Account/Login?backUrl={0}", backUrl);
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

                //#region 校验权限
                ////校验权限
                //if (UserInfo.UName == "admin")
                //    return;

                ////获取当前的请求地址
                //string url = HttpContext.Request.RawUrl.ToLower();
                //string method = HttpContext.Request.HttpMethod.ToLower();

                //IApplicationContext ctx = ContextRegistry.GetContext();
                //IActionInfoService actionInfoService = ctx.GetObject("ActionInfoService") as IActionInfoService;
                //IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService = ctx.GetObject("R_UserInfo_ActionInfoService") as IR_UserInfo_ActionInfoService;
                //IUserInfoService UserInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;


                //var actionInfo = actionInfoService.GetEntities(a => a.Url.ToLower() == url && a.HttpMethod.ToLower() == method).FirstOrDefault();

                //if (actionInfo == null)
                //    filterContext.HttpContext.Response.Redirect("/Error/Error404");//跳转到错误页面

                //var rUas = R_UserInfo_ActionInfoService.GetEntities(u => u.UserInfoId == UserInfo.Id);

                //var item = (from a in rUas where a.ActionInfoId == actionInfo.Id select a).FirstOrDefault();

                //if (item != null)
                //{
                //    if (item.HasPermission)
                //        return;
                //    else
                //        filterContext.HttpContext.Response.Redirect("/Error/Error404");//跳转到错误页面
                //}

                ////第2条线
                //var user = UserInfoService.GetEntities(u => u.Id == UserInfo.Id).FirstOrDefault();
                //var allRole = from a in user.RoleInfo select a;
                //var actions = from r in allRole from a in r.ActionInfo select a;
                //var tem = (from t in actions where t.Id == actionInfo.Id select t).Count();
                //if (tem <= 0)
                //    filterContext.HttpContext.Response.Redirect("/Error/Error404");//跳转到错误页面
                //#endregion
                base.OnActionExecuting(filterContext);
            }
        }
    }
}