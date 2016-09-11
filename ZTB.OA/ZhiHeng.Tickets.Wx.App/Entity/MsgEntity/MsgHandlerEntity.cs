using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatApi.Entity.MsgEntity
{
    /// <summary>
    ///消息处理程序实体
    /// </summary>
    public class MsgHandlerEntity
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MsgType { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public EventType EventType { get; set; }
        public Action<BaseMsg> Action { get; set; }
        /// <summary>
        /// 消息处理程序列表
        /// </summary>
        public static List<MsgHandlerEntity> MsgHandlerEntities;
    }
}
