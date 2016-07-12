using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Configuration;
using ZhiHeng.Tickets.Wx.Sy.Repository;
using ZhiHeng.Tickets.Wx.Sy.Repository.Models;

namespace ZhiHeng.Tickets.Wx.Sy.Repository
{
    public class APIToken
    {
        public static string host = ConfigurationManager.AppSettings["webapi"];
        static string userName = ConfigurationManager.AppSettings["userName"];
        static string passWord = ConfigurationManager.AppSettings["passWord"];


        static DateTime getAccessTokenTime;//获取AccessToken的时间 
        static AccessToken acessToken = new AccessToken();

        public static AccessToken AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(acessToken.access_token) || DateTime.Now > getAccessTokenTime.AddSeconds(acessToken.expires_in - 60))
                {
                    string url = string.Format("{0}{1}", host, "/token");
                    string parameter = string.Format("username={0}&password={1}&scope=common&grant_type=password", userName, passWord);//传入参数，目前使用的是Json格式
                    HttpContent content = new StringContent(parameter);
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "MTIzNDU2OmFiY2RlZg==");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                        HttpResponseMessage response = client.PostAsync(url, content).Result;
                        string result = response.Content.ReadAsStringAsync().Result;
                        acessToken = JsonConvert.DeserializeObject<AccessToken>(result);//反序列json
                        getAccessTokenTime = DateTime.Now;
                    }
                }
                return acessToken;
            }
        }
    }
}
