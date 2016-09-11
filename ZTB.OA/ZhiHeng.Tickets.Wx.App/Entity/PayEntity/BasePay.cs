using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatApi.Entity.PayEntity
{
    /// <summary>
    /// 微信支付基类
    /// </summary>
    public abstract class BasePay
    {
        /// <summary>
        ///微信分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 随机字符串，不长于32位
        /// </summary>
        public string nonce_str { get; set; }
    }
}
