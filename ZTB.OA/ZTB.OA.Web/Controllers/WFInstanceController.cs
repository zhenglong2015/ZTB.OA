using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.IBLL;
using ZTB.OA.Model;

namespace ZTB.OA.Web.Controllers
{
    public class WFInstanceController : BaseController
    {
        public IWP_TempService WP_TempService { get; set; }

        public IUserInfoService UserInfoService { get; set; }
        // GET: WFInstance
        public ActionResult Index()
        {
            IList<WP_Temp> list = WP_TempService.GetEntities(w => true).ToList();
            return View(list);
        }

        /// <summary>
        /// 发起流程，流程ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add(int id)
        {
            var temp = WP_TempService.GetEntities(w => w.Id == id).FirstOrDefault();
            var allUsers = UserInfoService.GetEntities(u => true).ToList();
            ViewData["UserList"] = (from u in allUsers select new SelectListItem() { Selected = false, Text = u.UName, Value = u.Id + "" }).ToList();
            return View(temp);
        }

        /// <summary>
        /// 发起流程
        /// </summary>
        /// <returns></returns>
        public ActionResult Add(WF_Instance instance,int id,int flowTo)
        {
            var currentUserId = base.UserInfo.Id;
            //在工作流实例中添加一条实例
            instance.DelFag = "0";
            instance.StartTime = DateTime.Now;
            instance.StartBy = currentUserId;
            instance.Level = 0;      


            return View();
        }
    }
}