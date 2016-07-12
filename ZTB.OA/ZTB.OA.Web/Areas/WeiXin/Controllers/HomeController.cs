using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZhiHeng.Tickets.Wx.App.Entity;
using ZhiHeng.Tickets.Wx.App.Entity.MsgEntity;
using ZhiHeng.Tickets.Wx.App.Service;

namespace ZTB.OA.Web.Areas.WeiXin.Controllers
{
    /// <summary>
    /// 微信接入类
    /// </summary>
    public class HomeController : Controller
    {
        static EnterParam param;
        public void Index()
        {
            try
            {
                string aesKey = ConfigurationManager.AppSettings["encodingAesKey"];
                //如果param为null，则实例化
                param = param ?? new EnterParam
                {
                    Appid = ConfigurationManager.AppSettings["appid"],
                    EncodingAESKey = aesKey,
                    Token = ConfigurationManager.AppSettings["token"],
                    IsAes = string.IsNullOrEmpty(aesKey)
                };

                var ip = HttpContext.Request.UserHostAddress;
                var ipEntity = WXService.GetIpArray();
                if (ipEntity != null && !ipEntity.ip_list.Contains(ip))
                {
                    HttpContext.Response.Write("非法请求");
                    return;
                }



                if (HttpContext.Request.HttpMethod == "GET")
                {
                    WXService.ValidUrl(param.Token);
                }
                else
                {
                    //处理微信消息
                    if (!WXService.ValidUrl(param.Token)) return;
                    if (MsgHandlerEntity.MsgHandlerEntities == null)
                    {
                        MsgHandlerEntity.MsgHandlerEntities = new List<MsgHandlerEntity>();

                        #region 文本消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.TEXT,
                            Action = Handler.TextHandler
                        });
                        #endregion
                        #region 图片消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.IMAGE,
                            Action = Handler.ImgHandler
                        });
                        #endregion

                        #region 语音消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.VOICE,
                            Action = Handler.VoiceHandler
                        });
                        #endregion

                        #region 视频消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.VIDEO,
                            Action = Handler.VideoHandler
                        });
                        #endregion

                        #region 地理位置消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.LOCATION,
                            Action = Handler.LoctionHandler
                        });
                        #endregion

                        #region 链接消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.LINK,
                            Action = Handler.linkHandler
                        });
                        #endregion

                        #region 群发消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.MASSSENDJOBFINISH,
                            Action = Handler.GroupJobHandler
                        });
                        #endregion

                        #region 模板消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.TEMPLATESENDJOBFINISH,
                            Action = Handler.TemplateJobHandler
                        });
                        #endregion

                        #region 扫描带参数二维码消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.SCAN,
                            Action = Handler.SanQrEventHandler
                        });
                        #endregion

                        #region 自定义菜单Click消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.CLICK,
                            Action = Handler.ClickEventHandler
                        });
                        #endregion

                        #region 自定义菜单View消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.VIEW,
                            Action = Handler.ViewEventHandler
                        });
                        #endregion

                        #region 自定义菜单扫描二维码（scancode_push）消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.SCANCODE_PUSH,
                            Action = Handler.MenuSanQrEventHandler
                        });
                        #endregion

                        #region 自定义菜单扫描二维码（scancode_waitmsg）消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.SCANCODE_WAITMSG,
                            Action = Handler.MenuSanQrEventHandler
                        });
                        #region 自定义菜单扫描二维码（scancode_waitmsg）消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.SCANCODE_WAITMSG,
                            Action = Handler.MenuSanQrEventHandler
                        });
                        #endregion
                        #endregion

                        #region 自定义菜单发送图片（pic_sysphoto）消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.PIC_SYSPHOTO,
                            Action = Handler.MenuSendPicEventHandler
                        });
                        #endregion
                        #region 自定义菜单发送图片（pic_photo_or_album）消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.PIC_PHOTO_OR_ALBUM,
                            Action = Handler.MenuSendPicEventHandler
                        });
                        #endregion

                        #region 自定义菜单发送图片（pic_weixin）消息处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.PIC_WEIXIN,
                            Action = Handler.MenuSendPicEventHandler
                        });
                        #endregion
                        #region 关注事件处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.SUBSCRIBE,
                            Action = Handler.SubscribeHandler
                        });
                        #endregion
                        #region 取消关注事件处理绑定
                        MsgHandlerEntity.MsgHandlerEntities.Add(new MsgHandlerEntity
                        {
                            MsgType = MsgType.EVENT,
                            EventType = EventType.UNSUBSCRIBE,
                            Action = Handler.SubscribeHandler
                        });
                        #endregion
                    }

                    MsgFactory.LoadMsg(param, MsgHandlerEntity.MsgHandlerEntities);
                }
            }
            catch (Exception e)
            {
                Response.Write("<script>alert('" + e.Message + "')</script>");
            }
        }
    }
}