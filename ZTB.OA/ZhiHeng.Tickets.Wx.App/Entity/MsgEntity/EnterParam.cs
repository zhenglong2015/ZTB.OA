using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeChatApi.Entity.MsgEntity
{
    /// <summary>
    /// 微信接入参数
    /// </summary>
    public class EnterParam
    {
        /// <summary>
        /// 是否加密
        /// </summary>
        public bool IsAes { get; set; }
        /// <summary>
        /// 接入token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        ///微信appid
        /// </summary>
        public string Appid { get; set; }
        /// <summary>
        /// 加密密钥
        /// </summary>
        public string EncodingAESKey { get; set; }
    }
}