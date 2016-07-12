using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiHeng.Tickets.Wx.Sy.Repository.Models
{
    public class OrderWaper
    {
        public OrderWaper()
        {
            Code = -1;
        }

        public int Code { get; set; }
        public string Message { get; set; }
        public string OrderId { get; set; }
        public int Quantity { get; set; }


        public int UserId { get; set; }
        public DateTime Time { get; set; }
        public int LinkId { get; set; }
        public int AdminId { get; set; }
        public bool IsSuccess { get; set; }
        public string ShareList { get; set; }
        public bool IsPay { get; set; }
        public DateTime PayTime { get; set; }
        public string WxPayId { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
