using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZTB.OA.Web.Models;

namespace ZTB.OA.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // filters.Add(new MyExceptionFilterAttrbut());
        }
    }
}
