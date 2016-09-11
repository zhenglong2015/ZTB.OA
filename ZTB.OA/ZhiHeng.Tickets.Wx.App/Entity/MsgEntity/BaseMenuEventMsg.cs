using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatApi.Entity.MsgEntity
{
    /// <summary>
    /// 菜单事件基础实体
    /// </summary>
    public class BaseMenuEventMsg : EventMsg
    {
        /// <summary>
        /// 事件key值
        /// </summary>
        public string EventKey { get; set; }
    }
}
