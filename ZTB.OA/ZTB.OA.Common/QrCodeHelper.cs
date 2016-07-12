// ==========================================
// Author                  :    WIN-JH13BJM99UM 
// Create Time           :    2016/7/12 13:28:19
// Update Time          :    2016/7/12 13:28:19
// ==========================================
// Class Description     :    
// ==========================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace ZTB.OA.Common
{
    public static class QrCodeHelper
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="logoImagepath">logo路径</param>
        /// <param name="qRCode">二维码尺寸</param>
        /// <param name="qRCodeVersion">二维码版本</param>
        /// <param name="logoSize">logo大小</param>
        /// <returns></returns>
        public static byte[] CreateQrCode(string content, string logoImagepath = "", int qRCode = 4, int qRCodeVersion = 7, int logoSize = 30)
        {
            QRCodeEncoder qrEncoder = new QRCodeEncoder();
            qrEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrEncoder.QRCodeScale = qRCode;
            qrEncoder.QRCodeVersion = qRCodeVersion;
            qrEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

            Bitmap qrcode = qrEncoder.Encode(content, Encoding.UTF8);
            Bitmap map = new Bitmap(qrcode.Width + 30, qrcode.Height + 30);

            //花白边
            Graphics g = Graphics.FromImage(map);
            g.FillRectangle(Brushes.White, 0, 0, map.Width, map.Height);
            g.DrawImage(qrcode, new Point(15, 15));


            if (!logoImagepath.Equals(string.Empty))
            {
                g = Graphics.FromImage(qrcode);
                Bitmap bitmapLogo = new Bitmap(logoImagepath);
                bitmapLogo = new Bitmap(bitmapLogo, new Size(logoSize, logoSize));
                PointF point = new PointF(qrcode.Width / 2 - logoSize / 2, qrcode.Height / 2 - logoSize / 2);
                g.DrawImage(bitmapLogo, point);
            }

            MemoryStream stream = new MemoryStream();
            map.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Seek(0, 0);

            return stream.ToArray();
        }

        /// <summary>
        /// 解析二维码
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string DecodeQRCode(string fileName)
        {
            QRCodeDecoder qrDecoder = new QRCodeDecoder();
            Bitmap map = new Bitmap(fileName);
            QRCodeImage qrImage = new QRCodeBitmapImage(map);
            return qrDecoder.decode(qrImage, Encoding.UTF8);
        }
    }
}