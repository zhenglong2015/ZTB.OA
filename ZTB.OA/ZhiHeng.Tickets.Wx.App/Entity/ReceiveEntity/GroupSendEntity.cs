using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiHeng.Tickets.Wx.App.Entity.ReceiveEntity
{
    /// <summary>
    /// 群发接口返回值
    /// </summary>
    public class GroupSendEntity : ErrorEntity
    {
        /// <summary>
        ///消息ID
        /// </summary>
        public string msg_id { get; set; }
    }
}
