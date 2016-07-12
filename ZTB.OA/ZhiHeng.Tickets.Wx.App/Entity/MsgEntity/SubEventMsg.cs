using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiHeng.Tickets.Wx.App.Entity.MsgEntity
{
    public class SubEventMsg : EventMsg
    {
        //基类中已经包括了常规订阅事件中所有的属性，
        private string eventkey;
        /// <summary>
        /// 扫描带参数二维码关注时，二维码的参数
        /// </summary>
        public string EventKey
        {
            get { return eventkey; }
            set { eventkey = value.Replace("qrscene_", ""); }
        }
        /// <summary>
        /// 二维码ticket，可换取二维码
        /// </summary>
        public string Ticket { get; set; }
    }
}
