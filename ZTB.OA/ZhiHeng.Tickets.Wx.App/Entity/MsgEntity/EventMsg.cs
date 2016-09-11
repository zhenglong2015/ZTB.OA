using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatApi.Entity.MsgEntity
{
    public class EventMsg : BaseMsg
    {
        public EventType Event { get; set; }
    }
}
