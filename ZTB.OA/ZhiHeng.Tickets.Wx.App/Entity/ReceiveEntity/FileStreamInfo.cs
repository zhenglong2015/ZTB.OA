using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WeChatApi.Entity.ReceiveEntity
{
    /// <summary>
    /// 文件流信息
    /// </summary>
    public class FileStreamInfo : MemoryStream
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
    }
}
