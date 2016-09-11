using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatApi.Entity.PayEntity
{
    /// <summary>
    /// 企业付款实体
    /// </summary>
    public class EPaymentRes : BasePayRes
    {
        /// <summary>
        /// 微信分配的公众账号ID
        /// </summary>
        public string mch_appid { get; set; }

        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 微信支付分配的终端设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 随机字符串，不长于32位
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 商户订单号，需保持唯一性
        /// </summary>
        public string partner_trade_no { get; set; }
        
        /// <summary>
        /// 企业付款成功，返回的微信订单号
        /// </summary>
        public string payment_no { get; set; }

        /// <summary>
        /// 企业付款成功时间
        /// </summary>
        public string payment_time { get; set; }
    }
}
