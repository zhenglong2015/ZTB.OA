using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.IBLL;
using ZTB.OA.Model;
using PagedList;
using ZTB.OA.Model.Param;
using System.Configuration;
using PagedList.Mvc;

namespace ZTB.OA.Web.Controllers
{
    public class UserInfoController : BaseController
    {
        // GET: UserInfo
        public IUserInfoService UserInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }

        public IRUserActionInfoService RUserActionInfoService { get; set; }

        public ActionResult List(int? page, string name, string pwd)
        {
            var users = UserInfoService.LoagPageData(new UserQueryParam()
            {
                Name = name,
                Pwd = pwd
            });
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("DataTablePartial", users.ToPagedList(page ?? 1, base.PageSize))
                : View(users.ToPagedList(page ?? 1, base.PageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserInfo user)
        {
            var res = UserInfoService.AddUser(user);
            return res.IsSuccess ? Content("ok:" + res.Message) : Content("no:" + res.Message);
        }

        public ActionResult Modify(int id)
        {
            var user = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult Modify(UserInfo user)
        {
            return UserInfoService.Update(user) ? Content("ok") : Content("no");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault().Name == "admin") return Content("no:此管理员不能删除");
            return UserInfoService.DeleteByLogical(id) ? Content("ok:删除成功") : Content("no:删除失败");
        }

        public ActionResult SetRole(int id)
        {
            var user = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewBag.AllRoles = RoleInfoService.GetEntities(r => !r.DelFag).ToList();
            ViewBag.ExitRoles = user.RoleInfo.Select(u => u.Id).ToList();
            return View(user);
        }

        [HttpPost]
        public ActionResult SetRolePost(int uId)
        {
            List<int> setRoleList = new List<int>();
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("ckb"))
                {
                    int role = int.Parse(key.Replace("ckb_", ""));
                    setRoleList.Add(role);
                }
            }
            RoleInfoService.SetRoles(uId, setRoleList);
            return Content("ok");
        }

        public ActionResult SetAction(int id)
        {

            ViewBag.user = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            var action = ActionInfoService.GetEntities(a => !a.DelFag).OrderBy(a => a.Id).ToList();
            return View(action);
        }
        [HttpPost]
        public ActionResult DeleteAction(int uId, int actinId)
        {
            var rua = RUserActionInfoService.GetEntities(r => r.ActionInfoId == actinId && r.UserInfoId == uId).FirstOrDefault();
            if (rua != null)
            {
                rua.DelFag = true;
                RUserActionInfoService.Update(rua);
            }
            return Content("ok");
        }
        [HttpPost]
        public ActionResult SetActionPost(int uId, int id, int val)
        {
            var rua = RUserActionInfoService.GetEntities(r => r.ActionInfoId == id && r.UserInfoId == uId).FirstOrDefault();
            if (rua != null)
            {
                rua.DelFag = false;
                rua.HasPermission = val == 1;
                RUserActionInfoService.Update(rua);
            }
            else
            {
                RUserActionInfo ruai = new RUserActionInfo();
                ruai.ActionInfoId = id;
                ruai.UserInfoId = uId;
                ruai.HasPermission = val == 1;
                RUserActionInfoService.Add(ruai);
            }
            return Content("ok");
        }
    }
}