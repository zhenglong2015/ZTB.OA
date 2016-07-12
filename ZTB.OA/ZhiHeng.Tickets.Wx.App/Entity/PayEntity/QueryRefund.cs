using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiHeng.Tickets.Wx.App.Entity.PayEntity
{
    public class QueryRefund:BasePayRes
    {
        public string transaction_id { get; set; }
        public string out_trade_no { get; set; }
        public string device_info { get; set; }
        public string out_refund_no { get; set; }
        public string refund_id { get; set; }
        public string sign { get; set; }
    }
}
