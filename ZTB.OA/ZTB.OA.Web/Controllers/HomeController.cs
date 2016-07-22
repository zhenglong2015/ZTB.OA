using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.IBLL;

namespace ZTB.OA.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public IUserInfoService UserInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        public ActionResult Index()
        {
            //LoadMenu();
            return View();
        }

        /// <summary>
        /// 获取用户的权限菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadMenu()
        {
            var id = base.UserInfo.Id;
            var user = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            var allRoles = user.RoleInfo;
            var allAcions = from r in allRoles from a in r.ActionInfo select a.Id;

            //拒绝的权限
            var allDenyAction = (from ura in user.RUserActionInfo
                                 where ura.HasPermission == false
                                 select ura.ActionInfoId).ToList();
            //去除拒绝的权限
            var allUserActionIds = (from a in allAcions where !allDenyAction.Contains(a) select a).ToList();

            //取出直接允许的权限
            var allAllowAction = (from ura in user.RUserActionInfo
                                  where ura.HasPermission
                                  select ura.ActionInfoId).ToList();

            //合并权限
            allUserActionIds.AddRange(allAllowAction);
            //去重
            allUserActionIds.Distinct();

            var actionList = ActionInfoService.GetEntities(a => allUserActionIds.Contains(a.Id) && a.IsMenu).ToList();

            return Content("ok");

        }
    }
}