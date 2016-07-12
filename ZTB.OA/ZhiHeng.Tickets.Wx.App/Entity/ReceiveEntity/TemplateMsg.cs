using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiHeng.Tickets.Wx.App.Entity.ReceiveEntity
{
    /// <summary>
    /// 模板消息调用后，返回实体
    /// </summary>
    public class TemplateMsg : ErrorEntity
    {
        public string msgid { get; set; }
    }
}
