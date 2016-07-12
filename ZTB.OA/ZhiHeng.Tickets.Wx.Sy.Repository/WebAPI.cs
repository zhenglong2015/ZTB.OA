using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using ZhiHeng.Tickets.Wx.Sy.Repository.Models;

namespace ZhiHeng.Tickets.Wx.Sy.Repository
{
    public class WebAPI
    {
        /// <summary>
        /// 获取可销售的产品
        /// </summary>
        /// <returns></returns>
        public static ProductWaper QueryProudcts()
        {
            ProductWaper ret = null;
            string url = string.Format("{0}{1}", APIToken.host, "OneCardParkApi/Product/QueryProudcts");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIToken.AccessToken.token_type, APIToken.AccessToken.access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(url).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<ProductWaper>(result);
            }
            return ret;
        }
        /// <summary>
        /// 订票
        /// </summary>
        /// <param name="ow">订单及订单明细集合（组合票多个订明细）
        /// 传人参数：OrderWaper中  OrderDetail (订单明细) 集合中传入订单明细的：ProjectID：项目ID，SellPlanID：销售计划ID ， ProductID：产品ID   Price：单价  RealPrice：实收价  
        /// 这些参数都是从 查询可销售产品中获得！
        /// <returns></returns>
        public static OrderWaper Order(OrderWaper ow)
        {
            OrderWaper ret = new OrderWaper();
            string url = string.Format("{0}OneCardParkApi/Product/Order", APIToken.host);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIToken.AccessToken.token_type, APIToken.AccessToken.access_token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync(url, ow).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<OrderWaper>(result);
            }
            return ret;
        }
    }
}
