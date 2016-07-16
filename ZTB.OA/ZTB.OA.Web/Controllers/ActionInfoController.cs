using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ZTB.OA.IBLL;
using ZTB.OA.Model;

namespace ZTB.OA.Web.Controllers
{
    public class ActionInfoController : BaseController
    {

        public IActionInfoService ActionInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }
        public ActionResult Index(int? page)
        {
            var actions = ActionInfoService.GetEntities(a => true).OrderByDescending(a => a.Id);
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
            actionInfo.ModifiedOn = DateTime.Now;
            actionInfo.SubTime = DateTime.Now;

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
            if (ActionInfoService.Update(actionInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = ActionInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            if (ActionInfoService.Delete(user))
            {
                return Content("ok");
            }
            else
                return Content("no");
        }

        public ActionResult SetRole(int id)
        {
            var action = ActionInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewBag.AllRoles = RoleInfoService.GetEntities(r => true).ToList();
            ViewBag.ExitRoles = action.RoleInfo.Select(u => u.Id).ToList();
            return View(action);
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
            ActionInfoService.SetRoles(uId, setRoleList);
            return Content("ok");
        }
    }
}