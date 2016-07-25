using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebGrease.Css.Extensions;
using ZTB.OA.IBLL;
using ZTB.OA.Model;

namespace ZTB.OA.Web.Controllers
{
    public class ActionInfoController : BaseController
    {

        public IActionInfoService ActionInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public ActionResult Index(int? page, string name)
        {
            var actions = ActionInfoService.GetEntities(a =>!a.DelFag);
            if (!string.IsNullOrEmpty(name))
            {
                actions = actions.Where(a => a.ActionName.Contains(name));
            }
            actions = actions.OrderByDescending(a => a.Id);
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("DataTablePartial", actions.ToPagedList(page ?? 1, base.PageSize))
                : View(actions.ToPagedList(page ?? 1, base.PageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ActionInfo actionInfo)
        {
            actionInfo.ModifyOn = DateTime.Now;
            ActionInfoService.Add(actionInfo);
            return Content("ok");
        }

        public ActionResult Modify(int id)
        {
            var user = ActionInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult Modify(ActionInfo actionInfo)
        {
            return ActionInfoService.Update(actionInfo) ? Content("ok") : Content("no");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return ActionInfoService.DeleteByLogical(id) ? Content("ok") : Content("no");
        }

        public ActionResult SetRole(int id)
        {
            var action = ActionInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewBag.AllRoles = RoleInfoService.GetEntities(r =>!r.DelFag).ToList();
            ViewBag.ExitRoles = action.RoleInfo.Select(u => u.Id).ToList();
            return View(action);
        }

        [HttpPost]
        public ActionResult SetRolePost(int uId)
        {
            List<int> setRoleList = new List<int>();

            Request.Form.AllKeys.ForEach(k =>
            {
                if (k.StartsWith("ckb"))
                {
                    setRoleList.Add(int.Parse(k.Replace("ckb_", "")));
                }
            });
            ActionInfoService.SetRoles(uId, setRoleList);
            return Content("ok");
        }
    }
}