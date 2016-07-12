using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiHeng.Tickets.Wx.Sy.Repository.Models
{
    public class OrderDetail
    {
        public OrderDetail()
        {
            Code = -1;
        }

        public int Code { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// 订单明细ID
        /// </summary>
        public string OrderDetailID { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public Decimal Price { get; set; }
        /// <summary>
        /// 实收
        /// </summary>
        public Decimal RealPrice { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// 销售计划ID
        /// </summary>
        public int SellPlanID { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 验票二维码
        /// </summary>
        public string ValidateNum { get; set; }

        public int ValidateState { get; set; }

        /// <summary>
        /// 微信端订单Id,
        /// 原样返回
        /// </summary>
        public string WxOrderDetailId { get; set; }

    }
}
