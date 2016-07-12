using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiHeng.Tickets.Wx.App.Entity.ReceiveEntity
{
    /// <summary>
    /// 短链接实体
    /// </summary>
    public class ShortUrl : ErrorEntity
    {
        /// <summary>
        /// 短链接
        /// </summary>
        public string short_url { get; set; }
    }
}
