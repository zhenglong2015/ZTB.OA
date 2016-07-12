using System;
using System.Web;
using System.Web.Security;
using ZhiHeng.Tickets.Wx.App.Entity.ReceiveEntity;
using System.Configuration;

namespace ZhiHeng.Tickets.Wx.App.Service
{
    /// <summary>
    /// 微信基础服务
    /// </summary>
    public class WXService
    {
        static DateTime getAccessTokenTime;//获取AccessToken的时间 
        static AccessToken acessToken = new AccessToken();
        /// <summary>
        /// 微信接入验证
        /// </summary>
        public static bool ValidUrl(string token)
        {
            var signature = HttpContext.Current.Request["signature"];
            var timestamp = HttpContext.Current.Request["timestamp"];
            var nonce = HttpContext.Current.Request["nonce"];
            var echostr = HttpContext.Current.Request.QueryString["echostr"];
            string[] temp = { token, timestamp, nonce };
            Array.Sort(temp);
            var tempStr = string.Join("", temp);
            var tempSign = FormsAuthentication.HashPasswordForStoringInConfigFile(tempStr, "SHA1").ToLower();
            if (tempSign == signature)
            {
                HttpContext.Current.Response.Write(echostr);
                return true;
            }
            return false;
        }

        public static string AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(acessToken.access_token) || DateTime.Now > getAccessTokenTime.AddSeconds(acessToken.expires_in - 60))
                {
                    var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",
                        ConfigurationManager.AppSettings["appid"], ConfigurationManager.AppSettings["secret"]
                       // "wx84a11c0f05462c04", "8a16e4205ca7723df4ce1670eaf77f59"
                       );
                    acessToken = Utils.GetResult<AccessToken>(url);
                    getAccessTokenTime = DateTime.Now;
                }
                return acessToken.access_token;
            }
        }
        /// <summary>
        /// 获取微信服务器IP列表
        /// </summary>
        public static IPEntity GetIpArray()
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}", AccessToken);
            return Utils.GetResult<IPEntity>(url);
        }

        /// <summary>
        /// 将一条长链接转成短链接
        /// </summary>
        /// <param name="longurl">长链接支持http://、https://、weixin://wxpay 格式的url</param>
        /// <returns>包含短连接和错误代码的实体</returns>
        public static ShortUrl LongUrlToShort(string longUrl)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/shorturl?access_token={0}", AccessToken);
            var json = new { action = "long2short", long_url = longUrl };
            return Utils.PostResult<ShortUrl>(json, url);
        }
    }
}
