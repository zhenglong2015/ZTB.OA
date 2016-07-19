using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.IBLL;
using PagedList;
using ZTB.OA.Model;
using System.Linq.Expressions;

namespace ZTB.OA.Web.Controllers
{
    public class RoleInfoController : BaseController
    {
        public IRoleInfoService RoleInfoService { get; set; }
        public ActionResult Index(int? page, string roleName)
        {
            var roles = RoleInfoService.GetEntities(r => true);
            if (!string.IsNullOrEmpty(roleName))
            {
                roles = roles.Where(r => r.RoleName.Contains(roleName));
            }
            roles = roles.OrderBy(r => r.Id);
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("DataTablePartial", roles.ToPagedList(page ?? 1, base.PageSize))
                : View(roles.ToPagedList(page ?? 1, base.PageSize));
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RoleInfo role)
        {

            role.ModifiedOn = DateTime.Now;
            role.SubTime = DateTime.Now;
            role.DelFag = "1";

            RoleInfoService.Add(role);
            return Content("ok");
        }

        public ActionResult Modify(int id)
        {
            var user = RoleInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult Modify(RoleInfo role)
        {
            if (RoleInfoService.Update(role))
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
            var user = RoleInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            if (RoleInfoService.Delete(user))
            {
                return Content("ok");
            }
            else
                return Content("no");
        }
    }
}
