using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiHeng.Tickets.Wx.App.Entity.ReceiveEntity;
using ZhiHeng.Tickets.Wx.App.Entity.SendEntity;

namespace ZhiHeng.Tickets.Wx.App.Service
{
    public static class SendMsgService
    {
        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="touser">要发送的用户的openid</param>
        /// <param name="template_id">模板ID</param>
        /// <param name="topcolor">消息卡片顶部的颜色</param>
        /// <param name="dataKeys">模板字段列表</param>
        /// <param name="url">点击消息卡片跳转的地址。默认为空，如果为空，ios设置会跳转到空白页面，安卓则不跳转</param>
        /// <returns>调用成功后，返回的实体的msgid属性指的是此条模板消息的id</returns>
        public static TemplateMsg SendTemplateMsg(string touser, string template_id, string topcolor, Dictionary<string, TemplateKey> dataKeys, string url = "")
        {
            var turl = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", WXService.AccessToken);
            var json = new
            {
                touser = touser,
                template_id = template_id,
                url = url,
                topcolor = topcolor,
                data = dataKeys
            };
            return Utils.PostResult<TemplateMsg>(json, turl);
        }
        /// <summary>
        /// 发送文本
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static ErrorEntity SendText(string openid, string content)
        {
            var json = new
            {
                touser = openid,
                msgtype = "text",
                text = new
                {
                    content = content
                }
            };
            return Send(json);
        }
        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public static ErrorEntity SendImg(string openid, string media_id)
        {
            var json = new
            {
                touser = openid,
                msgtype = "image",
                image = new
                {
                    media_id = media_id
                }
            };
            return Send(json);
        }
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public static ErrorEntity SendVoice(string openid, string media_id)
        {
            var json = new
            {
                touser = openid,
                msgtype = "voice",
                voice = new
                {
                    media_id = media_id
                }
            };
            return Send(json);
        }
        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="Video"></param>
        /// <returns></returns>
        public static ErrorEntity SendVideo(string openid, CustomVideo Video)
        {
            var json = new
            {
                touser = openid,
                msgtype = "video",
                video = Video
            };
            return Send(json);
        }
        /// <summary>
        /// 发送音乐消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="music"></param>
        /// <returns></returns>
        public static ErrorEntity SendMusic(string openid, CustomMusic music)
        {
            var json = new
            {
                touser = openid,
                msgtype = "music",
                music = music
            };
            return Send(json);
        }
        /// <summary>
        /// 发送图文消息 图文消息条数限制在8条以内，注意，如果图文数超过8，则将会无响应。
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        public static ErrorEntity SendArticle(string openid, CustomArticles article)
        {
            var json = new
            {
                touser = openid,
                msgtype = "news",
                news = article
            };
            return Send(json);
        }

        private static ErrorEntity Send(object obj)
        {
            return Utils.PostResult<ErrorEntity>(obj, string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}",WXService.AccessToken));
        }
    }
}
