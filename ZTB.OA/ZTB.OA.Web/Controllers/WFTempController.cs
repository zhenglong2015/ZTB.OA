using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.IBLL;
using ZTB.OA.Model;

namespace ZTB.OA.Web.Controllers
{
    public class WFTempController : BaseController
    {
        /// <summary>
        /// 显示模板
        /// </summary>
        /// <returns></returns>

        public IWP_TempService WP_TempService { get; set; }

        public ActionResult Index(int? page)
        {
            var roles = WP_TempService.GetEntities(w => true).OrderByDescending(w => w.Id);
            return View(roles.ToPagedList(page ?? 1, base.PageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(WP_Temp wpTemp)
        {
            wpTemp.DelFag = "0";
            wpTemp.SubTime = DateTime.Now;
            WP_TempService.Add(wpTemp);
            return Content("ok");
        }

        public ActionResult Modify(int id)
        {
            var temp = WP_TempService.GetEntities(u => u.Id == id).FirstOrDefault();
            return View(temp);
        }
        [HttpPost]
        public ActionResult Modify(WP_Temp wpTemp)
        {
            if (WP_TempService.Update(wpTemp))
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
            var temp = WP_TempService.GetEntities(u => u.Id == id).FirstOrDefault();
            if (WP_TempService.Delete(temp))
            {
                return Content("ok");
            }
            else
                return Content("no");
        }
    }
}