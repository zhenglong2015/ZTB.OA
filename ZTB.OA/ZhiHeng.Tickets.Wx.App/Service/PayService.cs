using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiHeng.Tickets.Wx.App.Entity.PayEntity;
using System.Web;

namespace ZhiHeng.Tickets.Wx.App.Service
{
    /// <summary>
    /// 微信支付相关的操作
    /// </summary>
    public class PayService
    {
        public static string key = System.Configuration.ConfigurationManager.AppSettings["key"];
        /// <summary>
        /// 调用统一下单接口并解析返回的XML
        /// </summary>
        /// <returns>响应实体</returns>
        public static UnifiedRes UnifiedOrder(UnifiedEntity entity)
        {
            var url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            UnifiedRes unified = PayRequest<UnifiedRes>(entity, url);
           //LogHelper.WriteInfoLog(string.Format("统一下单:body{0},detail:{1},out_trade_no:{2},spbill_create_ip:{3},total_fee:{4},notify_url:{5},openid:{6},result_code:{7},return_code:{8},return_msg:{9}",
           //    entity.body, entity.detail, entity.out_trade_no, entity.spbill_create_ip, entity.total_fee, entity.notify_url, entity.openid, unified.result_code, unified.return_code, unified.return_msg));
            return unified;
        }
        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="reFund">退款实体</param>
        /// <param name="key">支付密钥</param>
        /// <param name="certpath">证书路径</param>
        /// <param name="certpwd">证书密码</param>
        /// <returns></returns>
        /// 
        public static ReFundRes ReFund(ReFund reFund, string certpath, string certpwd)
        {
            var url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
            ReFundRes refund = PayRequest<ReFundRes>(reFund, url, certpath, certpwd);
           //LogHelper.WriteInfoLog(string.Format("申请退款:op_user_id{0},out_refund_no:{1},out_trade_no:{2},refund_fee:{3},total_fee:{4},transaction_id:{5},result_code:{6},return_code:{7},return_msg:{8}",
           //   reFund.op_user_id, reFund.out_refund_no, reFund.out_trade_no, reFund.refund_fee, reFund.total_fee,
           //   refund.transaction_id, refund.result_code, refund.return_code, refund.return_msg));
            return refund;
        }
        /// <summary>
        /// 查询退款
        /// </summary>
        public static QueryRefundRes QueryReFund(QueryRefund refund)
        {
            var url = "https://api.mch.weixin.qq.com/pay/refundquery";
            return PayRequest<QueryRefundRes>(refund, url);
        }
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="orderQuery">订单查询实体</param>
        /// <param name="key">支付密钥</param>
        public static OrderQueryRes QueryOrder(QueryOrder queryOrder)
        {
            var url = "https://api.mch.weixin.qq.com/pay/orderquery";
            return PayRequest<OrderQueryRes>(queryOrder, url);
        }
        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="closeOrder"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static CloseOrderRes CloseOrder(CloseOrder closeOrder)
        {
            var url = "https://api.mch.weixin.qq.com/pay/closeorder";
            return PayRequest<CloseOrderRes>(closeOrder, url);
        }
        /// <summary>
        /// 获取通用回调
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callBack"></param>
        public void GetNotifyRes(Action<OrderInfo> callBack)
        {
            try
            {
                var reqdata = Utils.GetRequestData();
                var rev = Utils.XmlToEntity<OrderInfo>(reqdata);
                if (rev.return_code != "SUCCESS")
                {
                    BackMessage("通讯错误"); return; 
                }
                if (rev.result_code != "SUCCESS")
                { 
                    BackMessage("业务出错"); return;
                }
                if (rev.sign == Utils.GetPaySign(rev, key))
                {
                    //回调函数，业务逻辑处理，处理结束后返回信息给微信
                    callBack(rev);
                }
            }
            catch (Exception e)
            {
                BackMessage("回调函数处理错误");
               //LogHelper.WriteErrorLog(e.Message);
            }
        }
        public static void BackMessage(string msg = "")
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (msg != "")
            {
                dic.Add("return_code", "FAIL");
                dic.Add("return_msg", msg);
                HttpContext.Current.Response.Write(Utils.ParseXML(dic));
            }
            else
            {
                dic.Add("return_code", "SUCCESS");
                HttpContext.Current.Response.Write(Utils.ParseXML(dic));
            }
        }

        ///// <summary>
        ///// 微信支付接口请求
        ///// </summary>
        ///// <typeparam name="T">返回值类型</typeparam>
        ///// <param name="obj">请求实体</param>
        ///// <param name="key">支付密钥</param>
        ///// <param name="url">接口地址</param>
        ///// <param name="certpath">证书路径，为null时说明不适用证书</param>
        ///// <param name="certpwd">证书密码</param>
        public static T PayRequest<T>(object obj, string url, string certpath = "", string certpwd = "")
        {
            var param = Utils.EntityToDictionary(obj);//将实体转换为数据集合
            param.Add("sign", Utils.GetPaySign(param, key));
            var xml = Utils.ParseXML(param);
            return Utils.XmlToEntity<T>(Utils.HttpPost(url, xml, certpath, certpwd));
        }
    }
}
