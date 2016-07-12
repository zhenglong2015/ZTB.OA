using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiHeng.Tickets.Wx.App.Entity.PayEntity
{
    public class UnifiedEntity:BasePay
    {
        /// <summary>
        /// 终端设备号(门店号或收银设备ID)，注意：PC网页或公众号内支付请传"WEB"
        /// </summary>
        public string device_info { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 商品或支付单简要描述
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 商品名称明细列表
        /// </summary>
        public string detail { get; set; }

        /// <summary>
        /// 附加数据，在查询API和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 商户系统内部的订单号,32个字符内、可包含字母
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 符合ISO 4217标准的三位字母代码，默认人民币：CNY，
        /// </summary>
        public string fee_type { get; set; }
        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        public int total_fee { get; set; }
        /// <summary>
        /// 终端IP
        /// </summary>
        public string spbill_create_ip { get; set; }
        /// <summary>
        /// 订单生成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010
        /// </summary>
        public string time_start { get; set; }
        /// <summary>
        /// 订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010
        /// </summary>
        public string time_expire { get; set; }
        /// <summary>
        /// 商品标记，代金券或立减优惠功能的参数
        /// </summary>
        public string goods_tag { get; set; }
        /// <summary>
        /// 接收微信支付异步通知回调地址，通知url必须为直接可访问的url，不能携带参数
        /// </summary>
        public string notify_url { get; set; }
        /// <summary>
        /// 交易类型 取值如下：JSAPI，NATIVE，APP
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 商品ID trade_type=NATIVE，此参数必传。此id为二维码中包含的商品ID，商户自行定义
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 指定支付方式 no_credit--指定不能使用信用卡支付
        /// </summary>
        public string limit_pay { get; set; }
        /// <summary>
        /// 用户标识	trade_type=JSAPI，此参数必传，用户在商户appid下的唯一标识
        /// </summary>
        public string openid { get; set; }
    }
}
