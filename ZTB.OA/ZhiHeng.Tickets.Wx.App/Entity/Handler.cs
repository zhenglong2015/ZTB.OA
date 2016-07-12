using ZhiHeng.Tickets.Wx.App.Entity.MsgEntity;
using System.Web;
using ZhiHeng.Tickets.Wx.App.Service;
using ZhiHeng.Tickets.Wx.App;
using ZhiHeng.Tickets.Wx.App.Entity.ReceiveEntity;
using System.Collections.Generic;


namespace ZhiHeng.Tickets.Wx.App.Entity
{
    /// <summary>
    /// 消息回复处理类
    /// </summary>
    public class Handler
    {
        #region 关注消息处理
        /// <summary>
        /// 关注消息处理
        /// </summary>
        public static void SubscribeHandler(BaseMsg obj)
        {
            bool isSend = false;
            if (!isSend)
            {
                obj.ResText(new EnterParam { }, "欢迎关注智恒游乐票务");
                isSend = true;
            }
        }
        #endregion

        #region 群发消息推送处理程序
        /// <summary>
        /// 群发消息推送处理程序
        /// </summary>
        /// <param name="baseMsg"></param>
        public static void GroupJobHandler(BaseMsg baseMsg)
        {
            //此处编写业务处理代码
        }
        #endregion

        #region 模板消息推送处理程序
        /// <summary>
        /// 模板消息推送处理程序
        /// </summary>
        /// <param name="baseMsg"></param>
        public static void TemplateJobHandler(BaseMsg baseMsg)
        {
            //此处编写业务处理代码
        }
        #endregion

        #region 扫描带参数二维码处理程序
        /// <summary>
        /// 扫描带参数二维码处理程序
        /// </summary>
        /// <param name="baseMsg"></param>
        public static void SanQrEventHandler(BaseMsg baseMsg)
        {
            //此处编写业务处理代码

        }
        #endregion

        #region 自定义菜单扫描二维码处理程序
        /// <summary>
        /// 自定义菜单扫描二维码处理程序
        /// </summary>
        /// <param name="baseMsg"></param>
        public static void MenuSanQrEventHandler(BaseMsg baseMsg)
        {
            //此处编写业务处理代码
        }
        #endregion

        #region 自定义菜单发送图片处理程序
        /// <summary>
        /// 自定义菜单发送图片处理程序
        /// </summary>
        /// <param name="baseMsg"></param>
        public static void MenuSendPicEventHandler(BaseMsg baseMsg)
        {
            //此处编写业务处理代码
        }
        #endregion

        #region 文本消息处理程序
        /// <summary>
        /// 文本消息处理程序
        /// </summary>
        public static void TextHandler(BaseMsg baseMsg)
        {

        }
        #endregion

        #region 图片消息处理程序
        /// <summary>
        /// 图片消息处理程序
        /// </summary>
        public static void ImgHandler(BaseMsg baseMsg)
        {


        }
        #endregion

        #region 视频消息处理程序
        /// <summary>
        /// 视频消息处理程序
        /// </summary>
        public static void VideoHandler(BaseMsg baseMsg)
        {


        }
        #endregion

        #region 语音消息处理程序
        /// <summary>
        /// 语音消息处理程序
        /// </summary>
        public static void VoiceHandler(BaseMsg baseMsg)
        {


        }
        #endregion

        #region 链接消息处理程序
        /// <summary>
        /// 链接消息处理程序
        /// </summary>
        public static void linkHandler(BaseMsg baseMsg)
        {


        }
        #endregion

        #region 地理位置消息处理程序
        /// <summary>
        /// 地理位置消息处理程序
        /// </summary>
        public static void LoctionHandler(BaseMsg baseMsg)
        {


        }
        #endregion

        #region 订单消息事件推送

        public static void OrderHandler(BaseMsg baseMsg)
        {


        }
        #endregion

        #region 自定义菜单Click消息处理
        /// <summary>
        /// 自定义菜单Click消息处理
        /// </summary>
        /// <param name="baseMsg"></param>
        public static void ClickEventHandler(BaseMsg baseMsg)
        {
            //BaseMenuEventMsg bmem = (BaseMenuEventMsg)baseMsg;
            //switch (bmem.EventKey)
            //{
            //    case "btnHistory"://历史消息
            //        MaterialList materialList = MaterialService.GetList(MaterialType.news, 0, 20);
            //        List<MaterialItem> list = materialList.item;
            //        if (list.Count > 0)
            //        {
            //            list.ForEach(a => { GroupSendService.SendArticleByOpenID(a.media_id, bmem.FromUserName); });
            //        }
            //        break;
            //    default:
            //        break;
            //}
        }
        #endregion

        #region 自定义菜单View消息处理
        /// <summary>
        /// 自定义菜单View消息处理
        /// </summary>
        /// <param name="baseMsg"></param>
        public static void ViewEventHandler(BaseMsg baseMsg)
        {

        }
        #endregion
    }
}