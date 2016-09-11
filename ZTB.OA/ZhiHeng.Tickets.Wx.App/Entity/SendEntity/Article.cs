using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeChatApi.Entity.SendEntity
{
    /// <summary>
    /// 图文素材图文项实体
    /// </summary>
    public class Article
    {
        /// <summary>
        /// 图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 图文消息的作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 在图文消息页面点击“阅读原文”后的页面
        /// </summary>
        public string content_source_url { get; set; }
        /// <summary>
        /// 图文消息页面的内容，支持HTML标签
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 图文消息的描述
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 是否显示封面，1为显示，0为不显示
        /// </summary>
        public int show_cover_pic { get; set; }
        /// <summary>
        /// 图文页的URL，或者，当获取的列表是图片素材列表时，该字段是图片的URL
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 图文消息的封面图片的地址，第三方开发者也可以使用这个URL下载图片到自己服务器中，然后显示在自己网站上
        /// </summary>
        public string thumb_url {get;set; }
    }
}
