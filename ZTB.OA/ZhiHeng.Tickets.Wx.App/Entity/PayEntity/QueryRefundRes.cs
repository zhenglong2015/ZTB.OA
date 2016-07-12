using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiHeng.Tickets.Wx.App.Entity.PayEntity
{
   public class QueryRefundRes:BasePayRes
    {
        public string transaction_id { get; set; }
        public string out_trade_no { get; set; }
        public string total_fee { get; set; }
        public string fee_type { get; set; }
        public string cash_fee { get; set; }
        public string cash_fee_type { get; set; }
        public string refund_fee { get; set; }
        public string coupon_refund_fee { get; set; }
        public string refund_count { get; set; }
        public List<RefundInfo> infos { get; set; }
    }
   public class RefundInfo
   {
       public string out_refund_no { get; set; }
       public string refund_id { get; set; }
       public string refund_channel { get; set; }
       public string refund_fee { get; set; }
       public string fee_type { get; set; }
       public string coupon_refund_fee { get; set; }
       /// <summary>
       /// 代金券或立减优惠使用数量
       /// </summary>
       public string coupon_refund_count { get; set; }
       public string refund_status { get; set; }
   }
}
