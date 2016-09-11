using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatApi.Entity.PayEntity
{
    /// <summary>
    /// 退款请求实体
    /// </summary>
    public class ReFund:BasePay
    {
        /// <summary>
        /// 终端设备号
        /// </summary>
        public string device_info { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 微信订单号 微信生成的订单号，在支付通知中有返回
        /// </summary>
        public string transaction_id { get; set; }
        /// <summary>
        /// 商户订单号 商户侧传给微信的订单号 
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 商户退款单号 商户系统内部的退款单号，商户系统内部唯一，同一退款单号多次请求只退一笔
        /// </summary>
        public string out_refund_no { get; set; }
        /// <summary>
        /// 订单总金额，单位为分，只能为整数
        /// </summary>
        public int total_fee { get; set; }
        /// <summary>
        /// 退款总金额，订单总金额，单位为分，只能为整数
        /// </summary>
        public int refund_fee { get; set; }
        /// <summary>
        /// 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string refund_fee_type { get; set; }
        /// <summary>
        /// 操作员帐号, 默认为商户号
        /// </summary>
        public string op_user_id { get; set; }
    }
}
