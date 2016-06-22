using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTB.OA.Common
{
    /// <summary>
    ///ValidateCode 的摘要说明
    /// </summary>
    public class ValidateCode
    {
        //验证码长度
        public int codeLen = 4;
        //图片清晰度
        public int sightRate = 55;
        //图片宽度
        public int imgWidth = 135;
        //图片高度
        public int imgHeight = 33;
        //字体家族名称
        public string fontFamily = "Times New Roman";
        //字体大小
        public int fontSize = 20;
        //字体样式
        public int fontStyle = 1;
        //绘制起始坐标X
        public int posX = 35;
        //绘制起始坐标Y
        public int posY = 0;
        //生成的验证码
        public string strValidateCode = null;

        public ValidateCode()
        {

        }

        public Bitmap CreateValidateCode()
        {
            string validateCode = GetValidateCode();//生成验证码

            Bitmap bitmap = new Bitmap(imgWidth, imgHeight);//生成BITMAP图像

            DisturbBitmap(bitmap);//图像背景

            DrawValidateCode(bitmap, validateCode);//绘制验证码图像

            return bitmap;
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        private string GetValidateCode()
        {
            string validateCode = "";
            Random random = new Random();
            for (int i = 0; i < codeLen; i++)
            {
                int n = random.Next(10);
                validateCode += n.ToString();
            }
            this.strValidateCode = validateCode;
            return validateCode;
        }

        /// <summary>
        /// 图像背景
        /// </summary>
        /// <param name="bitmap"></param>
        private void DisturbBitmap(Bitmap bitmap)
        {
            Random random = new Random();
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    if (random.Next(90) <= this.sightRate)
                        bitmap.SetPixel(i, j, Color.White);
                }
            }
        }

        /// <summary>
        /// 绘制验证码图像
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="validateCode"></param>
        private void DrawValidateCode(Bitmap bitmap, string validateCode)
        {
            Graphics g = Graphics.FromImage(bitmap);//获取绘制器对象
            Font font = new Font(fontFamily, fontSize, FontStyle.Bold);//设置绘制字体
            g.DrawString(validateCode, font, Brushes.Black, posX, posY);//绘制验证码图像
        }
    }
}
