using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatApi.Entity.PayEntity
{
    /// <summary>
    /// 关闭订单请求实体
    /// </summary>
    public class CloseOrder : BasePay
    {
        /// <summary>
        /// 商户系统内部的订单号
        /// </summary>
        public string out_trade_no { get; set; }
        public string sign { get; set; }
    }
}
