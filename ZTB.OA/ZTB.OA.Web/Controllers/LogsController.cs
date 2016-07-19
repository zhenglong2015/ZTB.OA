using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.Common.Logs;

namespace ZTB.OA.Web.Controllers
{
    public class LogsController : BaseController
    {
        // GET: Logs
        public ActionResult Index()
        {
            string absoluteLogDirPath = Server.MapPath(ConfigurationManager.AppSettings["logDirPath"].ToString());
            if (System.IO.Directory.Exists(absoluteLogDirPath))
            {
                ViewBag.Response = LogReadService.GetDirFiles(absoluteLogDirPath, "*.*");
            }
            return View();
        }


        public ActionResult GetContent(string path)
        {
            var response =LogReadService.ReadContent(path);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        public  ActionResult Remove(string path)
        {
            var response = LogReadService.Delete(path);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}