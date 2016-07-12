using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiHeng.Tickets.Wx.App.Entity.PayEntity
{
    /// <summary>
    /// 支付接口调用后，响应参数基础类。
    /// </summary>
    public class BasePayRes : BasePay
    {
        /// <summary>
        /// 返回状态码 SUCCESS/FAIL此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 返回信息，如非空，为错误原因签名失败参数格式校验错误
        /// </summary>
        public string return_msg { get; set; }
        /// <summary>
        /// 微信返回的随机字符串
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 微信返回的签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 业务结果SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }
        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }
    }
}
