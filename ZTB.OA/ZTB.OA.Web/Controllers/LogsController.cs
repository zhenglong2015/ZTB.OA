using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.Common.Logs;
using PagedList;
using PagedList.Mvc;

namespace ZTB.OA.Web.Controllers
{
    public class LogsController : BaseController
    {
        // GET: Logs
        public ActionResult Index(int? pageNumber)
        {
            string absoluteLogDirPath = Server.MapPath(ConfigurationManager.AppSettings["logDirPath"]);
            if (System.IO.Directory.Exists(absoluteLogDirPath))
            {
                var response = LogReadService.GetDirFiles(absoluteLogDirPath, "*.*");
                if (response.Model.Count > 0)
                    return Request.IsAjaxRequest() ? (ActionResult)PartialView(response.Model.ToPagedList(pageNumber ?? 1, base.PageSize))
                        : View(response.Model.ToPagedList(pageNumber ?? 1, base.PageSize));
            }
            return View();
        }


        public async Task<ActionResult> GetContent(string path)
        {
            var response =await LogReadService.ReadContent(path);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Remove(string path)
        {
            var response = LogReadService.Delete(path);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}