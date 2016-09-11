using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatApi.Entity.PayEntity
{
    public class EPaymentQuery:BasePay
    {
        /// <summary>
        /// 生成签名方式查看3.2.1节 
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 商户调用企业付款API时使用的商户订单号 
        /// </summary>
        public string partner_trade_no { get; set; }
    }
}
