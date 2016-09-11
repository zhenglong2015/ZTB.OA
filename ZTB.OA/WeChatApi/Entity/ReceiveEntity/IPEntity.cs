using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatApi.Entity.ReceiveEntity
{
   public class IPEntity:ErrorEntity
    {
       /// <summary>
       /// IP列表
       /// </summary>
       public string[] ip_list { get; set; }
    }
}
