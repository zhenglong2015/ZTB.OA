using System.Web;
using System.Web.Mvc;
using ZTB.OA.Portal.Models;

namespace ZTB.OA.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyExceptionFilterAttrbut());
        }
    }
}
