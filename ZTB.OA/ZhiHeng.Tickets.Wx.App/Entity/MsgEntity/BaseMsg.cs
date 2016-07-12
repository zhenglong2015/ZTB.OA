using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using ZhiHeng.Tickets.Wx.App.Entity.SendEntity;
using ZhiHeng.Tickets.Wx.App.Common;

namespace ZhiHeng.Tickets.Wx.App.Entity.MsgEntity
{
    public class BaseMsg
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MsgType { get; set; }

        /// <summary>
        /// 回复文本
        /// </summary>
        public virtual void ResText(EnterParam param, string content)
        {
            var resxml = new StringBuilder();
            resxml.AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", FromUserName);
            resxml.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", ToUserName);
            resxml.AppendFormat("<CreateTime>{0}</CreateTime>", Utils.ConvertDateTimeInt(DateTime.Now));
            resxml.Append("<MsgType><![CDATA[text]]></MsgType>");
            resxml.AppendFormat("<Content><![CDATA[{0}]]></Content></xml>", content);
            Response(param, resxml.ToString());
        }

        /// <summary>
        /// 回复图文
        /// </summary>
        public void ResArticles(EnterParam param, List<ResArticle> artList)
        {
            var resxml = new StringBuilder();
            resxml.AppendFormat("<xml><ToUserName><![CDATA[{0}]]></ToUserName>", FromUserName);
            resxml.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", ToUserName);
            resxml.AppendFormat("<CreateTime>{0}</CreateTime>", Utils.ConvertDateTimeInt(DateTime.Now));
            resxml.Append("<MsgType><![CDATA[news]]></MsgType>");
            resxml.AppendFormat("<ArticleCount>{0}</ArticleCount><Articles>", artList.Count);
            foreach (var article in artList)
            {
                resxml.AppendFormat("<item><Title><![CDATA[{0}]]></Title>", article.Title);
                resxml.AppendFormat("<PicUrl><![CDATA[{0}]]></PicUrl>", article.PicUrl);
                resxml.AppendFormat("<Url><![CDATA[{0}]]></Url>", article.Url);
                resxml.AppendFormat("<Description><![CDATA[{0}]]></Description></item>", article.Description);
            }
            resxml.Append("</Articles></xml>");
            Response(param, resxml.ToString());
        }


        private void Response(EnterParam param, string data)
        {
            if (param.IsAes)
            {
                var wxcpt = new WXBizMsgCrypt(param.Token, param.EncodingAESKey, param.Appid);
                wxcpt.EncryptMsg(data, Utils.ConvertDateTimeInt(DateTime.Now).ToString(), Utils.ConvertDateTimeInt(DateTime.Now).ToString(), ref data);
            }
            HttpContext.Current.Response.Write(data);
            HttpContext.Current.Response.End();
        }
    }
}
