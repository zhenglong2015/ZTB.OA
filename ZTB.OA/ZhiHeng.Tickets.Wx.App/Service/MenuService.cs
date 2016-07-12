using ZhiHeng.Tickets.Wx.App.Entity.ReceiveEntity;
using System.Web;
using System.Text.RegularExpressions;
using System.Linq;

namespace ZhiHeng.Tickets.Wx.App.Service
{
    /// <summary>
    /// 处理菜单相关的操作
    /// </summary>
    public class MenuService
    {

        private static readonly string menuPath = System.AppDomain.CurrentDomain.BaseDirectory + @"bin\Data\menu.txt";
        static string url = System.Configuration.ConfigurationManager.AppSettings["url"];
        static string appId = System.Configuration.ConfigurationManager.AppSettings["appid"];

        //private static readonly string menuPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\Data\menu.txt";
        //static string url = "zhenglong.com";
        //static string appId ="123456789";
        /// <summary>
        /// 获取菜单
        /// </summary>
        public static string QueryMenu()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", WXService.AccessToken);
            return Utils.HttpGet(url);
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        public static ErrorEntity CreateMenu(string menu)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", WXService.AccessToken);
            return Utils.PostResult<ErrorEntity>(url, menu);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        public static ErrorEntity DeleteMenu()
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", WXService.AccessToken);
            return Utils.GetResult<ErrorEntity>(url);
        }
        /// <summary>
        /// 加载菜单
        /// </summary>
        public static ErrorEntity LoadMenu()
        {
            string strMenu = Utils.Read(menuPath).Trim();
            string strReturn = string.Empty;
            //修改appid
            Regex regex = new Regex(@"(^|\?|&)appid=[A-Za-z0-9]+");
            MatchCollection matchCollection = regex.Matches(strMenu);
            var temStr = matchCollection[0].Value.Split('=')[1];
            strMenu = strMenu.Replace(temStr, appId);

            //修改域名
           regex = new Regex(@"^|redirect_uri=.{80}");
            matchCollection = regex.Matches(strMenu);
            temStr = matchCollection[1].Value.Substring(matchCollection[1].Value.IndexOf('=') + 1);
            string[] tem1 = temStr.Split('/');
            string oldUrl=(tem1[0] + "//" + tem1[2]).Trim();
            string newUrl=url.Trim();
            strReturn = strMenu.Replace(oldUrl,newUrl );

            Utils.Write(menuPath, strReturn);

            return CreateMenu(strReturn);
        }
    }
}
