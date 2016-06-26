using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZTB.OA.Web.Models
{
    public class MyExceptionFilterAttrbut:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            //自己处理异常信息
            Common.Logs.LogHelper.WriteErrorLog(filterContext.Exception.ToString());
            filterContext.RequestContext.HttpContext.Response.Redirect("Base/Error");
        }
    }
}