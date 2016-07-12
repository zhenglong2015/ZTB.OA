using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiHeng.Tickets.Wx.Sy.Repository.Models
{

    public class Product
    {

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 销售计划ID
        /// </summary>
        public int SellPlanID { get; set; }
        /// <summary>
        /// 销售计划名称
        /// </summary>
        public string SellPlanName { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public long ProductID { get; set; }
        /// <summary>
        /// 产品名称pi
        /// </summary>
        public String ProductName { get; set; }
        /// <summary>
        /// 票面价格
        /// </summary>
        public Decimal Price { get; set; }
        /// <summary>
        /// 批发价格-对平台的结算价格
        /// </summary>
        public Decimal RealPrice { get; set; }
        /// <summary>
        /// 票类型
        /// 每天 ;0
        /// 每周一;1
        /// 每周二;2
        /// 每周三;3
        /// 每周四;4
        /// 每周五;5
        /// 每周六;6
        /// 每周日;7
        /// 时间段;8
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 有效期开始日期（格式yyyy-MM-dd）
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 有效期结束日期（格式yyyy-MM-dd）
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 有效期开始时间（格式yyyy-MM-dd）
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 有效期结束时间（格式yyyy-MM-dd）
        /// </summary>
        public string EndTime { get; set; }

    }
}
