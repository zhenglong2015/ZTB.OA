using System;
using System.Web.Mvc;
using WeChatApi;
using WeChatApi.Entity.PayEntity;
using WeChatApi.Entity.ReceiveEntity;
using WeChatApi.Service;
using ZTB.OA.Web.Models;

namespace ZTB.OA.Web.Areas.WeiXin.Controllers
{
    public class BaseController : Controller
    {
        private static JsApiTicket ticket;
        public static JsEntity je = new JsEntity();

        private string QueryString(string name)
        {
            return HttpContext.Request.Form[name] == null ? string.Empty : HttpContext.Request.Form[name];
        }
        private int QueryInt(string name)
        {
            return HttpContext.Request.Form[name] == null ? 0 : int.Parse(HttpContext.Request.Form[name]);
        }

        #region 初始化微信配置
        /// <summary>
        /// 初始化微信配置
        /// </summary>
        /// <returns></returns>
        public void Init()
        {
            je.appId = WebWxConfigHelper.appid;
            je.timeStamp = WeChatApi.Utils.ConvertDateTimeInt(DateTime.Now).ToString();
            var url = WebWxConfigHelper.domainUrl + Request.RawUrl;
            if (ticket == null || ticket.expires_time < DateTime.Now)
                ticket = JsApi.GetHsJsApiTicket();
            je.paySign = JsApi.GetJsApiSign(je.timeStamp, ticket.ticket, je.timeStamp, url);
        }

        #endregion
        #region 关闭订单
        /// <summary>
        /// 关闭订单
        /// 以下情况需要调用关单接口：商户订单支付失败需要生成新单号重新发起支付，要对原订单号调用关单，避免重复支付；系统下单后，用户支付超时，系统退出不再受理，避免用户继续，请调用关单接口。
        ///注意：订单生成后不能马上调用关单接口，最短调用时间间隔为5分钟。
        /// </summary>
        /// <returns></returns>
        public ActionResult CloseOrder()
        {
            var query = new CloseOrder
            {
                appid = WebWxConfigHelper.appid,
                mch_id = WebWxConfigHelper.mch_id,
                nonce_str = WeChatApi.Utils.GetGuid(),
                out_trade_no = QueryString("out_trade_no")
            };

            var res = PayService.CloseOrder(query);
            if (res.result_code.ToUpper() == "SUCCESS" && Utils.ValidSign(res, res.sign, PayService.key) && res.return_code.ToUpper() == "SUCCESS")
            {
                return Json(new { status = "OK", info = "OK" });
            }
            else if (res.return_code.ToUpper() != "SUCCESS")
            {
                return Json(new { status = "FAIL", info = res.return_msg });
            }
            else if (res.result_code != "SUCCESS")
            {
                return Json(new { status = "FAIL", info = res.err_code_des });
            }
            else
            {
                return Json(new { status = "FAIL", info = "微信服务器返回的签名不合法！" });
            }
        }
        #endregion
        #region 调用统一下单
        /// <summary>
        /// 调用统一下单接口
        /// 商户系统先调用该接口在微信支付服务后台生成预支付交易单，返回正确的预支付交易回话标识后再按扫码、JSAPI、APP等不同场景生成交易串调起支付。
        /// </summary>
        /// <returns></returns>
        public UnifiedRes UnifiedOrder(PayType payType)
        {
            var query = new UnifiedEntity
            {
                appid = WebWxConfigHelper.appid,
                mch_id = WebWxConfigHelper.mch_id,
                nonce_str = WeChatApi.Utils.GetGuid(),
                body = QueryString("body"),
                detail = QueryString("detail"),
                out_trade_no = QueryString("out_trade_no"),
                spbill_create_ip = Utils.GetIP(),
                total_fee = QueryInt("total_fee"),
                notify_url = WebWxConfigHelper.anyscUrl,
                trade_type = payType.ToString()
            };
            if (payType == PayType.JSAPI)
            {
                //TODO:当前登录用户的openid
                //query.openid = this.UserInfos.OpenId;
            }
            else
            {
                //TODO:当前登录用户的openid
                //query.product_id = this.UserInfos.OpenId;
            }

            return PayService.UnifiedOrder(query);
        }
        #endregion
        #region 退款
        /// <summary>
        /// 退款
        /// 当交易发生之后一段时间内，由于买家或者卖家的原因需要退款时，卖家可以通过退款接口将支付款退还给买家，微信支付将在收到退款请求并且验证成功之后，按照退款规则将支付款按原路退到买家帐号上。
        ///注意：1交易时间超过一年的订单无法提交退款；
        ///2、微信支付退款支持单笔交易分多次退款，多次退款需要提交原支付订单的商户订单号和设置不同的退款单号。一笔退款失败后重新提交，要采用原来的退款单号。总退款金额不能超过用户实际支付金额。
        /// <param name="out_refund_no">商户退款单号 商户系统内部的退款单号，商户系统内部唯一，同一退款单号多次请求只退一笔 </param>
        /// <param name="out_trade_no">商户订单号 商户侧传给微信的订单号</param>
        /// <param name="refund_fee">退款金额 退款总金额，订单总金额，单位为分，只能为整数</param>
        /// <param name="total_fee">订单金额 订单总金额，单位为分，只能为整数</param>
        /// <param name="transaction_id">微信订单号 微信生成的订单号，在支付通知中有返回</param>
        /// 微信订房单和商户订单号 二选一
        /// <returns></returns>
        public ActionResult Refund(string out_refund_no, string out_trade_no, int refund_fee, int total_fee, string transaction_id)
        {
            var query = new ReFund
            {
                appid = WebWxConfigHelper.appid,
                mch_id = WebWxConfigHelper.mch_id,
                nonce_str = Utils.GetGuid(),
                op_user_id = WebWxConfigHelper.mch_id,
                out_refund_no = out_refund_no,
                out_trade_no = out_trade_no,
                refund_fee = refund_fee,
                total_fee = total_fee,
                transaction_id = transaction_id
            };
            var rev = PayService.ReFund(query, Server.MapPath("~/apiclient_cert.p12"), WebWxConfigHelper.mch_id);

            Common.Logs.LogHelper.WriteInfoLog(string.Format("退款:op_user_id{0},out_refund_no:{1},out_trade_no:{2},refund_fee:{3},total_fee:{4},transaction_id:{5},return_code:{6},result_code{7}",
                query.op_user_id, query.out_refund_no, query.out_trade_no, query.refund_fee, query.total_fee, query.transaction_id, rev.return_code, rev.result_code));

            if (rev.return_code.ToUpper() == "SUCCESS" && Utils.ValidSign(rev, rev.sign, PayService.key) && rev.result_code.ToUpper() == "SUCCESS")
            {

                #region 退款业务逻辑处理 TODO

                #endregion
                return Json(new { status = "OK", info = "OK" });
            }
            else if (rev.return_code.ToUpper() != "SUCCESS")
            {
                return Json(new { status = "FAIL", info = rev.return_msg });
            }
            else if (rev.result_code.ToUpper() != "SUCCESS")
            {
                return Json(new { status = "FAIL", info = rev.err_code_des });
            }
            else
            {
                return Json(new { status = "FAIL", info = "微信服务器返回的签名不合法！" });
            }
        }
        #endregion
        #region 订单查询
        /// <summary>
        /// 订单查询
        /// 该接口提供所有微信支付订单的查询，商户可以通过查询订单接口主动查询订单状态，完成下一步的业务逻辑。
        ///需要调用查询接口的情况：
        /// 当商户后台、网络、服务器等出现异常，商户系统最终未接收到支付通知；
        /// 调用支付接口后，返回系统错误或未知交易状态情况；
        ///调用被扫支付API，返回USERPAYING的状态；
        /// 调用关单或撤销接口API之前，需确认支付状态；
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderIsSuccess()
        {
            var query = new QueryOrder { appid = WebWxConfigHelper.appid, mch_id = WebWxConfigHelper.mch_id, nonce_str = Utils.GetGuid(), out_trade_no = QueryString("out_trade_no"), transaction_id = QueryString("transaction_id") };
            var info = PayService.QueryOrder(query);
            return Json(info);
        }
        #endregion
        #region JSAPI支付
        /// <summary>
        /// JSAPI支付
        /// </summary>
        /// <returns></returns>
        public ActionResult Jspay()
        {
            if (Utils.GetWxVersion() < 5)
            {
                return Json(new { status = "FAIL", info = "请升级微信版本!" });
            };
            var unifie = UnifiedOrder(PayType.JSAPI);
            if (unifie.result_code.ToUpper() == "SUCCESS" && unifie.result_code.ToUpper() == "SUCCESS" && Utils.ValidSign(unifie, unifie.sign, PayService.key))
            {
                var jsentity = new JsEntity
                {
                    appId = WebWxConfigHelper.appid,
                    nonceStr = Utils.GetGuid(),
                    package = "prepay_id=" + unifie.prepay_id,
                    signType = "MD5",
                    timeStamp = Utils.ConvertDateTimeInt(DateTime.Now).ToString()
                };
                jsentity.paySign = Utils.GetPaySign(jsentity, PayService.key);
                return Json(new { status = "OK", info = jsentity });
            }
            else if (unifie.result_code.ToUpper() != "SUCCESS")
            {
                return Json(new { status = "FAIL", info = unifie.return_msg });
            }
            else if (unifie.result_code.ToUpper() != "SUCCESS")
            {
                return Json(new { status = "FAIL", info = unifie.err_code_des });
            }
            else
            {
                return Json(new { status = "FAIL", info = "微信服务器返回的签名不合法！" });
            }
        }
        #endregion
        #region 企业付款
        /// <summary>
        /// 企业付款
        /// </summary>
        /// <param name="openid">收款人openid</param>
        /// <param name="check_name">校验用户姓名选项</param>
        /// <param name="re_user_name">收款用户姓名,如果check_name设置为FORCE_CHECK或OPTION_CHECK，则必填用户真实姓名</param>
        /// <param name="amount">金额（单位分）最小为100</param>
        /// <param name="desc">描述</param>
        /// <returns></returns>
        public EPaymentRes EnterprisePay(string openid, CheckNameOption check_name, string re_user_name, int amount, string desc)
        {
            var payment = new EPayment()
            {
                mch_appid = WebWxConfigHelper.appid,
                mchid = WebWxConfigHelper.mch_id,
                nonce_str = Utils.GetGuid(),
                partner_trade_no = Utils.GetGuid(),
                openid = openid,
                check_name = check_name,
                re_user_name = re_user_name,
                amount = amount,
                desc = desc,
                spbill_create_ip = Utils.GetIP()
            };
            return PayService.EnterprisePay(payment, Server.MapPath("~/apiclient_cert.p12"), WebWxConfigHelper.mch_id);
        }
        #endregion
        #region 企业付款查询
        /// <summary>
        /// 企业付款查询
        /// </summary>
        /// <param name="partner_trade_no">订单号</param>
        /// <returns></returns>
        public EPaymentQueryRes QueryEnterprisePay(string partner_trade_no)
        {
            var query = new EPaymentQuery { appid = WebWxConfigHelper.appid, mch_id = WebWxConfigHelper.mch_id, nonce_str = Utils.GetGuid(), partner_trade_no = partner_trade_no };
            return PayService.EnterprisePayQuery(query, Server.MapPath("~/apiclient_cert.p12"), WebWxConfigHelper.mch_id);
        }
        #endregion

        public enum PayType
        {
            /// <summary>
            /// 公众号支付
            /// </summary>
            JSAPI,
            /// <summary>
            /// 原生扫码支付
            /// </summary>
            NATIVE,
            /// <summary>
            /// app支付
            /// </summary>
            APP
        }
    }
}