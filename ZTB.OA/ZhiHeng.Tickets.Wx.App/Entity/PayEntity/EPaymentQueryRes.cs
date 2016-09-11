using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatApi.Entity.PayEntity
{
    public class EPaymentQueryRes : BasePayRes
    {
       
        //在return_code为SUCCESS的时候有返回
        //result_codeSUCCESS/FAIL  
        //err_code  错误码信息  
        //err_code_des 结果信息描述  


        //以下字段在return_code 和result_code都为SUCCESS的时候有返回

        /// <summary>
        /// 商户使用查询API填写的单号的原路返回. 
        /// </summary>
        public string partner_trade_no { get; set; }

        /// <summary>
        /// 微信支付分配的商户号 
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        ///  调用企业付款API时，微信系统内部产生的单号  
        /// </summary>
        public string detail_id { get; set; }

        /// <summary>
        /// SUCCESS:转账成功 
        /// FAILED:转账失败 
        /// PROCESSING:处理中
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 如果失败则有失败原因 
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 收款用户openid 
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 收款用户姓名
        /// </summary>
        public string transfer_name { get; set; }

        /// <summary>
        /// 付款金额单位分
        /// </summary>
        public int payment_amount { get; set; }

        /// <summary>
        /// 发起转账的时间
        /// </summary>
        public string transfer_time { get; set; }

        /// <summary>
        /// 付款时候的描述
        /// </summary>
        public string desc { get; set; }
    }
}
