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
        public ActionResult Index(int? page)
        {
            var actions = ActionInfoService.GetEntities(a => true).OrderByDescending(a => a.Id);
            return View(actions.ToPagedList(page ?? 1, base.PageSize));
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
    }
}