using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ZTB.OA.Web.Models
{
    public class MyExceptionFilterAttrbut:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
           // base.OnException(filterContext);
            HttpException httpException = new HttpException(null, filterContext.Exception);
            /*
             * 1、根据对应的HTTP错误码跳转到错误页面
             * 2、这里对HTTP 404/400错误进行捕捉和处理
             * 3、其他错误默认为HTTP 500服务器错误
             */
            if (httpException != null && (httpException.GetHttpCode() == 400 || httpException.GetHttpCode() == 404))
            {
                filterContext.HttpContext.Response.StatusCode = 404;
                filterContext.HttpContext.Response.RedirectToRoute(new {action = "Error404", controller = "Error"});
                    //.WriteFile("~/HttpError/404.html");
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.HttpContext.Response.RedirectToRoute(new { action= "Error500", controller= "Error" });//.WriteFile("~/HttpError/500.html");
            }
            /*---------------------------------------------------------
             * 这里可进行相关自定义业务处理，比如日志记录等
             ---------------------------------------------------------*/

            //自己处理异常信息
            Common.Logs.LogHelper.WriteErrorLog(filterContext.Exception.ToString());

            //设置异常已经处理,否则会被其他异常过滤器覆盖
            filterContext.ExceptionHandled = true;

            //在派生类中重写时，获取或设置一个值，该值指定是否禁用IIS自定义错误。
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

        }
    }
}